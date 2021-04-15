using DeezNuts.Data.Models;
using DeezNuts.Dtos;
using DeezNuts.Enums;
using DeezNuts.Managers.States.Interfaces;
using DeezNuts.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DeezNuts.Managers
{
    internal class ListeningState : IState
    {
        private readonly ITwilioService _twilioService;
        private readonly IListeningActionService _listeningActionService;
        private readonly IMessageService _messageService;

        public ListeningState(
            ITwilioService twilioService,
            IListeningActionService listeningActionService,
            IMessageService messageService)
        {
            _twilioService = twilioService;
            _listeningActionService = listeningActionService;
            _messageService = messageService;
        }

        public Customer DoAction(StateActionDto dto)
        {
            var customer = dto.Customer;
            var listeningActions = _listeningActionService.GetListeningActionsByMatch(dto.InputText);

            var priorities = new List<Func<MessageBuilderContext>> {
                () => NoMatch(customer, listeningActions),
                () => UniqueTypedMatch(customer, listeningActions),
                () => MultipleTypedMatches(customer, listeningActions),
                () => GeneralMatch(customer, listeningActions)
            };

            var mbContext = new MessageBuilderContext();
            foreach (var func in priorities)
            {
                mbContext = func();
                if (mbContext == null) continue; 
                else break;
            }

            mbContext.InputText = dto.InputText;

            _twilioService.SendMessage(new SendMessageDto
            {
                FromNumber = dto.MyNumber,
                ToNumber = customer.PhoneNumber
            }, mbContext);

            return customer;
        }

        private MessageBuilderContext NoMatch(Customer customer, IEnumerable<ListeningAction> listeningActions)
        {
            var typedActions = listeningActions.Where(l => l.GetType() == typeof(TypedListeningAction));
            var generalActions = listeningActions.Where(l => l.GetType() == typeof(GeneralListeningAction));

            if (typedActions.Count() == 0 && generalActions.Count() == 0)
            {
                var returnObj = new MessageBuilderContext();

                returnObj.Customer = customer;
                returnObj.SendText = _messageService.GetRandomTypedMessage(MessageType.ListeningActionResponseNoMatch).Text;

                return returnObj;
            }
            else return null;
        }

        private MessageBuilderContext UniqueTypedMatch(Customer customer, IEnumerable<ListeningAction> listeningActions)
        {
            var typedActions = listeningActions.Where(l => l.GetType() == typeof(TypedListeningAction));

            if (typedActions.Count() == 1)
            {
                var returnObj = new MessageBuilderContext();
                var typedAction = (TypedListeningAction)typedActions.First();

                returnObj.Customer = customer;
                returnObj.SendText = _messageService.GetRandomTypedMessage(typedAction.ResponseMessageType).Text;
                returnObj.Customer.Session.AwaitState = typedActions.First().NextState;

                return returnObj;
            }
            else return null;
        }

        private MessageBuilderContext MultipleTypedMatches(Customer customer, IEnumerable<ListeningAction> listeningActions)
        {
            var typedActions = listeningActions.Where(l => l.GetType() == typeof(TypedListeningAction));
            var returnObj = new MessageBuilderContext();

            if (typedActions.Count() > 1)
            {
                returnObj.Customer = customer;
                returnObj.SendText = _messageService.GetRandomTypedMessage(MessageType.ListeningActionResponseMultipleMatches).Text;
                returnObj.Customer.Session.AwaitState = typedActions.First().NextState;

                for(int i = 0; i < typedActions.Count() - 1; i++)
                    returnObj.ListeningActionMatches += $"{typedActions.ElementAt(i).Name}, ";
                returnObj.ListeningActionMatches += $"or {typedActions.LastOrDefault().Name}";

                return returnObj;
            }
            else return null;
        }

        private MessageBuilderContext GeneralMatch(Customer customer, IEnumerable<ListeningAction> listeningActions)
        {
            var generalActions = listeningActions.Where(l => l.GetType() == typeof(GeneralListeningAction));
            var returnObj = new MessageBuilderContext();

            if (generalActions.Count() >= 1)
            {
                var rand = new Random();
                int index = rand.Next(0, generalActions.Count());
                var actions = (GeneralListeningAction)generalActions.ElementAt(index);
                var responses = actions.Responses;

                index = rand.Next(0, responses.Count());

                var response = responses.ElementAt(index);

                returnObj.Customer = customer;
                returnObj.SendText = response.Text;

                return returnObj;
            }
            else return null;
        }
    }
}
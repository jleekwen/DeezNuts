using DeezNuts.Data.Models;
using DeezNuts.Dtos;
using DeezNuts.Enums;
using DeezNuts.Managers.States;
using DeezNuts.Managers.States.Interfaces;
using DeezNuts.Services;
using System.Collections.Generic;

namespace DeezNuts.Managers
{
    public class StateManager : IState
    {
        private readonly IGoogleAnalyticsService _googleAnalyticsService;
        private readonly IListeningActionService _listeningActionService;
        private readonly IMessageService _messageService;
        private readonly IOrderService _orderService;
        private readonly ITwilioService _twilioService;

        private Dictionary<SessionState, IState> _stateDictionary;

        public StateManager(
            IGoogleAnalyticsService googleAnalyticsService,
            IListeningActionService listeningActionService,
            IMessageService messageService,
            IOrderService orderService,
            ITwilioService twilioService)
        {
            _googleAnalyticsService = googleAnalyticsService;
            _listeningActionService = listeningActionService;
            _messageService = messageService;
            _orderService = orderService;
            _twilioService = twilioService;

            BuildDictionary();
        }

        public Customer DoAction(StateActionDto dto)
        {
            return _stateDictionary[dto.Customer.Session.AwaitState].DoAction(dto);
        }

        private void BuildDictionary()
        {
            _stateDictionary = new Dictionary<SessionState, IState>()
            {
                { SessionState.AwaitName, new AwaitNameState(_googleAnalyticsService, _twilioService, _messageService ) },
                { SessionState.GreetingNew, new GreetingNewState(_twilioService, _messageService) },
                { SessionState.GreetingReturning, new GreetingReturningState(_twilioService, _messageService) },
                { SessionState.Listening, new ListeningState(_twilioService, _listeningActionService, _messageService ) },
                { SessionState.Order, new OrderState(_twilioService, _googleAnalyticsService, _orderService) }
            };
        }
    }
}

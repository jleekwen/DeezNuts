using DeezNuts.Data.Models;
using DeezNuts.Dtos;
using DeezNuts.Enums;
using DeezNuts.Managers.States.Interfaces;
using DeezNuts.Services;
using System.Linq;
using static Google.Cloud.Language.V1.Entity.Types;

namespace DeezNuts.Managers.States
{
    public class AwaitNameState : IState
    {
        private readonly IGoogleAnalyticsService _googleAnalyticsService;
        private readonly ITwilioService _twilioService;
        private readonly IMessageService _messageService;

        public AwaitNameState(
            IGoogleAnalyticsService googleAnalyticsService,
            ITwilioService twilioService,
            IMessageService messageService)
        {
            _googleAnalyticsService = googleAnalyticsService;
            _twilioService = twilioService;
            _messageService = messageService;
        }

        public Customer DoAction(StateActionDto dto)
        {
            var customer = dto.Customer;
            string message = "";

            var response = _googleAnalyticsService.Analyze(dto.InputText);
            var person = response.Entities.FirstOrDefault(e => e.Type == Type.Person);

            if (person != null)
            {
                customer.Name = person.Name;
                message = _messageService.GetRandomTypedMessage(MessageType.RequestNameResponseSuccess).Text;
                customer.Session.AwaitState = SessionState.Listening;
            }
            else
            {
                message = _messageService.GetRandomTypedMessage(MessageType.RequestNameResponseFail).Text;
                customer.Session.AwaitState = SessionState.AwaitName;
            }

            _twilioService.SendMessage(new SendMessageDto
            {
                FromNumber = dto.MyNumber,
                ToNumber = customer.PhoneNumber
            }, new MessageBuilderContext
            {
                Customer = customer,
                InputText = dto.InputText,
                SendText = message
            }); ;

            return customer;
        }
    }
}

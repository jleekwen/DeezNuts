using DeezNuts.Data.Models;
using DeezNuts.Dtos;
using DeezNuts.Enums;
using DeezNuts.Managers.States.Interfaces;
using DeezNuts.Services;

namespace DeezNuts.Managers.States
{
    public class GreetingReturningState : IState
    {
        private readonly ITwilioService _twilioService;
        private readonly IMessageService _messageService;

        public GreetingReturningState(
            ITwilioService twilioService,
            IMessageService messageService)
        {
            _twilioService = twilioService;
            _messageService = messageService;
        }

        public Customer DoAction(StateActionDto dto)
        {
            var message = _messageService.GetRandomTypedMessage(MessageType.IntroGreetingNew).Text;
            var customer = dto.Customer;

            if (string.IsNullOrEmpty(customer.Name))
            {
                message += System.Environment.NewLine + System.Environment.NewLine + _messageService.GetRandomTypedMessage(MessageType.RequestName).Text;
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
            });

            return customer;
        }
    }
}

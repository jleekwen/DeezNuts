using DeezNuts.Data.Models;
using DeezNuts.Dtos;
using DeezNuts.Enums;
using DeezNuts.Managers.States.Interfaces;
using DeezNuts.Services;
using Google.Cloud.Language.V1;
using Microsoft.Extensions.Options;
using System.Linq;
using static Google.Cloud.Language.V1.Entity.Types;

namespace DeezNuts.Managers.States
{
    public class AwaitNameState : IState
    {
        private readonly DeezNutsConfig _config;
        private readonly ITwilioService _twilioService;
        private readonly IMessageService _messageService;

        public AwaitNameState(
            IOptions<DeezNutsConfig> config,
            ITwilioService twilioService,
            IMessageService messageService)
        {
            _config = config.Value;
            _twilioService = twilioService;
            _messageService = messageService;
        }

        public Customer DoAction(StateActionDto dto)
        {
            var customer = dto.Customer;
            string message = "";

            LanguageServiceClientBuilder builder = new LanguageServiceClientBuilder
            {
                CredentialsPath = _config.GoogleCredentialFile
            };

            var languageServiceClient = builder.Build();

            // TODO?: might have better results prepending question test
            // var document = Document.FromPlainText($"{_messageService.GetRandomMessageByType(MessageType.RequestName).Text} {dto.InputText}");
            var document = Document.FromPlainText($"{dto.InputText}");
            var response = languageServiceClient.AnalyzeEntities(document);
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

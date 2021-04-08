using DeezNuts.Dtos;
using DeezNuts.Managers.States.Interfaces;
using DeezNuts.Services;
using Microsoft.AspNetCore.Mvc;
using Twilio.AspNet.Common;
using Twilio.AspNet.Core;
using Twilio.TwiML;

namespace DeezNuts.Controllers
{
    [Route("[controller]")]
    public class SmsController : TwilioController
    {
        private readonly ICustomerService _customerService;
        private readonly IMessageLogService _messageLogService;
        private readonly IState _stateManager;

        public SmsController(
            ICustomerService customerService,
            IMessageLogService messageLogService,
            IState stateManager)
        {
            _customerService = customerService;
            _messageLogService = messageLogService;
            _stateManager = stateManager;
        }

        [HttpPost]
        public TwiMLResult Index(SmsRequest request)
        {
            var customer = _customerService.GetByPhoneNumber(request.From);

            _messageLogService.Create(new MessageLogDto(request));

            customer = _stateManager.DoAction(new StateActionDto
            {
                Customer = customer,
                MyNumber = request.To,
                InputText = request.Body
            });

            _customerService.Save(customer);
            return TwiML(new MessagingResponse());
        }

        [HttpPost("status")]
        public void Status(SmsStatusCallbackRequest request)
        {
            _messageLogService.Create(new MessageLogDto(request));
        }
    }
}

using DeezNuts.Data.Models;
using DeezNuts.Dtos;
using DeezNuts.Managers.States.Interfaces;
using DeezNuts.Services;

namespace DeezNuts.Managers
{
    internal class OrderState : IState
    {
        private readonly ITwilioService _twilioService;
        private readonly IGoogleAnalyticsService _googleAnalyticsService;
        private readonly IOrderService _orderService;

        public OrderState(
            ITwilioService twilioService,
            IGoogleAnalyticsService googleAnalyticsService,
            IOrderService orderService)
        {
            _twilioService = twilioService;
            _googleAnalyticsService = googleAnalyticsService;
            _orderService = orderService;
        }

        public Customer DoAction(StateActionDto dto)
        {
            var response = _googleAnalyticsService.Analyze(dto.InputText);

            throw new System.NotImplementedException();
        }
    }
}
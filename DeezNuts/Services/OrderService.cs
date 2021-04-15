using DeezNuts.Repositories;

namespace DeezNuts.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public string BuildOrderList(int customerId)
        {
            var order = _repository.GetCurrentOrder(customerId);
            var returnString = "";

            foreach (var item in order.Items)
            {
                returnString += item + System.Environment.NewLine;
            }

            return returnString.Trim();
            throw new System.NotImplementedException();
        }
    }
}

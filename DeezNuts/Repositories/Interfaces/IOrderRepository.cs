using DeezNuts.Data.Models;
using System.Collections.Generic;

namespace DeezNuts.Repositories
{
    public interface IOrderRepository
    {
        Order GetCurrentOrder(int customerId);
        Order GetOrderById(int id);
        void Update(Order order);
    }
}
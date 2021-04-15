using DeezNuts.Data;
using DeezNuts.Data.Models;
using DeezNuts.Enums;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DeezNuts.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DeezNutsContext _context;

        public OrderRepository(DeezNutsContext context)
        {
            this._context = context;
        }

        public Order GetCurrentOrder(int customerId)
        {
            return _context.Orders
                .Include(o => o.Items)
                .FirstOrDefault(o => o.CustomerId == customerId && o.Status != OrderStatus.Completed);
        }

        public Order GetOrderById(int id)
        {
            return _context.Orders
                .SingleOrDefault(o => o.Id == id);
        }

        public void Update(Order order)
        {
            var options = _context.options;

            using (var context = new DeezNutsContext(options))
            {
                if (order.Id > 0)
                {
                    var existingOrder = GetOrderById(order.Id);
                    context.Entry(existingOrder).CurrentValues.SetValues(order);
                    context.Entry(existingOrder).State = EntityState.Modified;
                }
                else
                    context.Orders.Add(order);

                context.SaveChanges();
            }
        }
    }
}

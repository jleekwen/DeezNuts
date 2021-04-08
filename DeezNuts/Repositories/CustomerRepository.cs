using DeezNuts.Data;
using DeezNuts.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DeezNuts.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DeezNutsContext _context;

        public CustomerRepository(DeezNutsContext context)
        {
            this._context = context;
        }

        public Customer GetCustomerByPhoneNumber(string phoneNumber)
        {
            return _context.Customers
                .Include(c => c.Session)
                .FirstOrDefault(c => c.PhoneNumber == phoneNumber);
        }

        public void Create(Customer customer)
        {
            var options = _context.options;

            using (var context = new DeezNutsContext(options))
            {
                if (customer.Id > 0)
                {
                    var existingCustomer = context.Customers.FirstOrDefault(c => c.Id == customer.Id);
                    //do the update to the database             
                    context.Entry(existingCustomer).CurrentValues.SetValues(customer);
                    context.Entry(existingCustomer).State = EntityState.Modified;

                    //then update the parent this way
                    //first get the particular parent for this student
                    var session = context.CustomerSessions.FirstOrDefault(s => s.CustomerId == existingCustomer.Id);

                    context.Entry(session).CurrentValues.SetValues(customer.Session);
                    context.Entry(session).State = EntityState.Modified;
                }
                else
                    context.Customers.Add(customer);

                context.SaveChanges();
            }
        }
    }
}

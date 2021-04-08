using DeezNuts.Data.Models;

namespace DeezNuts.Repositories
{
    public interface ICustomerRepository
    {
        Customer GetCustomerByPhoneNumber(string phoneNumber);
        void Create(Customer customer);
    }
}
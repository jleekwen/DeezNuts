using DeezNuts.Data.Models;

namespace DeezNuts.Services
{
    public interface ICustomerService
    {
        Customer GetByPhoneNumber(string phoneNumber);
        void Save(Customer customer);
    }
}
using DeezNuts.Data.Models;
using DeezNuts.Enums;
using DeezNuts.Repositories;
using System;

namespace DeezNuts.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly ISettingRepository _settingRepository;

        public CustomerService(
            ICustomerRepository repository,
            ISettingRepository settingRepository)
        {
            _repository = repository;
            _settingRepository = settingRepository;
        }

        public Customer GetByPhoneNumber(string phoneNumber)
        {
            var customer = _repository.GetCustomerByPhoneNumber(phoneNumber);

            if (customer == null)
            {
                customer = new Customer();
                customer.PhoneNumber = phoneNumber;
                customer.Session.AwaitState = SessionState.GreetingNew;
                Save(customer);
            }

            int.TryParse(_settingRepository.GetSettingByType(SettingType.SessionTimeoutMinutes).Value, out int timeoutTime);

            if (customer.Session.LastActivityDateTime.AddMinutes(timeoutTime) <= DateTime.Now)
            {
                customer.Session.LastActivityDateTime = DateTime.Now;
                customer.Session.AwaitState = SessionState.GreetingReturning;
            }

            return customer;
        }

        public void Save(Customer customer)
        {
            customer.Session.LastActivityDateTime = DateTime.Now;
            _repository.Create(customer);
        }

    }
}

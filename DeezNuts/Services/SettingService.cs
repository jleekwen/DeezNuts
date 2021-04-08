using DeezNuts.Data.Models;
using DeezNuts.Enums;
using DeezNuts.Repositories;

namespace DeezNuts.Services
{
    public class SettingService : ISettingService
    {
        private ISettingRepository _repository;

        public SettingService(ISettingRepository repository)
        {
            _repository = repository;
        }

        public Setting GetSettingByType(SettingType type)
        {
            return _repository.GetSettingByType(type);
        }
    }
}

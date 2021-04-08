using DeezNuts.Data.Models;
using DeezNuts.Enums;

namespace DeezNuts.Repositories
{
    public interface ISettingRepository
    {
        Setting GetSettingByType(SettingType type);
    }
}
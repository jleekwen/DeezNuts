using DeezNuts.Data.Models;
using DeezNuts.Enums;

namespace DeezNuts.Services
{
    public interface ISettingService
    {
        Setting GetSettingByType(SettingType type);
    }
}
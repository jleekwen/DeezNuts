using DeezNuts.Data;
using DeezNuts.Data.Models;
using DeezNuts.Enums;
using System.Linq;

namespace DeezNuts.Repositories
{
    public class SettingRepository : ISettingRepository
    {
        private readonly DeezNutsContext _context;

        public SettingRepository(DeezNutsContext context)
        {
            _context = context;
        }

        public Setting GetSettingByType(SettingType type)
        {
            return _context.Settings
                .SingleOrDefault(s => s.Type == type);
        }
    }
}

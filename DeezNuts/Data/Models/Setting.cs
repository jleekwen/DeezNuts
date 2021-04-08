using DeezNuts.Enums;

namespace DeezNuts.Data.Models
{
    public class Setting
    {
        public int Id { get; set; }
        public SettingType Type { get; set; }
        public string Value { get; set; }
    }
}

using DeezNuts.Data.Models;

namespace DeezNuts.Dtos
{
    public class MessageBuilderContext
    {
        public string InputText { get; set; }
        public string SendText { get; set; }
        public Customer Customer { get; set; }
        public string ListeningActionMatches { get; set; }
    }
}

using DeezNuts.Enums;
using System.Collections.Generic;

namespace DeezNuts.Data.Models
{
    public class ListeningAction : BaseModel
    {
        public string Name { get; set; }
        public string RegexMatch { get; set; }
        public SessionState NextState { get; set; }
    }

    public class TypedListeningAction : ListeningAction
    {
        public MessageType ResponseMessageType { get; set; }
    }

    public class GeneralListeningAction : ListeningAction
    {
        public ICollection<Message> Responses { get; set; }
    }
}

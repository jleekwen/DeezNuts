using DeezNuts.Enums;

namespace DeezNuts.Data.Models
{
    public class Message : BaseModel
    {
        public string Text { get; set; }
    }

    public class TypedMessage : Message
    {
        public MessageType Type { get; set; }
    }
}

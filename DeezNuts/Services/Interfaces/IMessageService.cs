using DeezNuts.Data.Models;
using DeezNuts.Enums;

namespace DeezNuts.Services
{
    public interface IMessageService
    {
        Message GetRandomTypedMessage(MessageType type);
    }
}
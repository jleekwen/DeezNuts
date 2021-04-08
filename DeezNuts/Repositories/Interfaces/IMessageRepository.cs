using DeezNuts.Data.Models;
using DeezNuts.Enums;
using System.Collections.Generic;

namespace DeezNuts.Repositories
{
    public interface IMessageRepository
    {
        IEnumerable<TypedMessage> GetMessagesByType(MessageType type);
    }
}
using DeezNuts.Data;
using DeezNuts.Data.Models;
using DeezNuts.Enums;
using System.Collections.Generic;
using System.Linq;

namespace DeezNuts.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DeezNutsContext _context;

        public MessageRepository(DeezNutsContext context)
        {
            this._context = context;
        }

        public IEnumerable<TypedMessage> GetMessagesByType(MessageType type)
        {
            return _context.TypedMessages
                .Where(m => m.Type == type)
                .ToList();
        }
    }
}

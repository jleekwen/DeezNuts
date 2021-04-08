using DeezNuts.Data;
using DeezNuts.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DeezNuts.Repositories
{
    public class MessageLogRepository : IMessageLogRepository
    {
        private readonly DeezNutsContext _context;

        public MessageLogRepository(DeezNutsContext context)
        {
            _context = context;
        }

        public void Create(MessageLog obj)
        {
            CreateOrUpdate(obj);
        }

        private void CreateOrUpdate(MessageLog obj)
        {
            var options = _context.options;

            var messageLog = _context.MessageLogs
                .SingleOrDefault(m => m.TwilioMessageSid == obj.TwilioMessageSid);

            using (var context = new DeezNutsContext(options))
            {
                if (messageLog != null)
                {
                    context.MessageLogs.Attach(new MessageLog
                    {
                        Id = messageLog.Id,
                        Status = obj.Status
                    });
                }
                else
                    context.MessageLogs.Add(obj);

                context.SaveChanges();
            }
        }
    }
}

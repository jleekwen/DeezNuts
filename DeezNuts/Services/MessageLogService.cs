using DeezNuts.Data.Models;
using DeezNuts.Dtos;
using DeezNuts.Repositories;

namespace DeezNuts.Services
{
    public class MessageLogService : IMessageLogService
    {
        private IMessageLogRepository _repository;

        public MessageLogService(IMessageLogRepository repository)
        {
            _repository = repository;
        }

        public void Create(MessageLog log)
        {
            _repository.Create(log);
        }

        public void Create(MessageLogDto log)
        {
            _repository.Create(new MessageLog
            {
                From = log.From,
                To = log.To,
                Message = log.Message,
                TwilioMessageSid = log.TwilioMessageSid,
                Status = log.Status
            });
        }
    }
}

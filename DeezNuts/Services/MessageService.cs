using DeezNuts.Data.Models;
using DeezNuts.Enums;
using DeezNuts.Repositories;
using System;
using System.Linq;

namespace DeezNuts.Services
{
    public class MessageService : IMessageService
    {
        private IMessageRepository _repository;

        public MessageService(IMessageRepository repository)
        {
            _repository = repository;
        }

        Message IMessageService.GetRandomTypedMessage(MessageType type)
        {
            var messages = _repository.GetMessagesByType(type);
            var rand = new Random();

            int index = rand.Next(0, messages.Count());
            return messages.ElementAt(index);
        }
    }
}

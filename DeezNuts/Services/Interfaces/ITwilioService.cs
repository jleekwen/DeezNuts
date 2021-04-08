using DeezNuts.Dtos;

namespace DeezNuts.Services
{
    public interface ITwilioService
    {
        void SendMessage(SendMessageDto dto, MessageBuilderContext mbContext);
    }
}
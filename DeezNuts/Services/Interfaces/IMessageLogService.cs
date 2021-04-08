using DeezNuts.Data.Models;
using DeezNuts.Dtos;

namespace DeezNuts.Services
{
    public interface IMessageLogService
    {
        void Create(MessageLog log);
        void Create(MessageLogDto log);
    }
}
using DeezNuts.Data.Models;

namespace DeezNuts.Repositories
{
    public interface IMessageLogRepository
    {
        void Create(MessageLog obj);
    }
}
using DeezNuts.Data.Models;
using System.Collections.Generic;

namespace DeezNuts.Repositories
{
    public interface IListeningActionRepository
    {
        IEnumerable<ListeningAction> GetListeningActionsByMatch(string match);
    }
}
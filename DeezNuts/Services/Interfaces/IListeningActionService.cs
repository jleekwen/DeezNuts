using DeezNuts.Data.Models;
using System.Collections.Generic;

namespace DeezNuts.Services
{
    public interface IListeningActionService
    {
        IEnumerable<ListeningAction> GetListeningActionsByMatch(string match);
    }
}
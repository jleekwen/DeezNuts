using DeezNuts.Data.Models;
using DeezNuts.Repositories;
using System.Collections.Generic;

namespace DeezNuts.Services
{
    public class ListeningActionService : IListeningActionService
    {
        private IListeningActionRepository _repository;

        public ListeningActionService(IListeningActionRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<ListeningAction> GetListeningActionsByMatch(string match)
        {
            return _repository.GetListeningActionsByMatch(match);
        }
    }
}

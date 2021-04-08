using DeezNuts.Data;
using DeezNuts.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DeezNuts.Repositories
{
    public class ListeningActionRepository : IListeningActionRepository
    {
        private readonly DeezNutsContext _context;

        public ListeningActionRepository(DeezNutsContext context)
        {
            this._context = context;
        }

        public IEnumerable<ListeningAction> GetListeningActionsByMatch(string match)
        {

            //NOTE: this is a workaround due to a bug in ef core https://github.com/dotnet/efcore/issues/16243
            var t = _context.TypedListeningActions
                .Select(l => (ListeningAction)l)
                .ToList()
                .Where(l => Regex.Match(match, l.RegexMatch, RegexOptions.IgnoreCase).Success);
            var g = _context.GeneralListeningActions
                .Include(l => l.Responses)
                .Select(l => (ListeningAction)l)
                .ToList()
                .Where(l => Regex.Match(match, l.RegexMatch, RegexOptions.IgnoreCase).Success);

            return t
                .Union(g);
        }
    }
}

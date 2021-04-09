using DeezNuts.Data;
using DeezNuts.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace DeezNuts.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DeezNutsContext _context;

        public ProductRepository(DeezNutsContext context)
        {
            this._context = context;
        }

        public IEnumerable<Product> GetActiveProducts()
        {
            return _context.Products
                .Where(p => p.IsActive)
                .ToList();
        }
    }
}

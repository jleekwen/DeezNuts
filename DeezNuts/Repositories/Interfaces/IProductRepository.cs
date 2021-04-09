using DeezNuts.Data.Models;
using System.Collections.Generic;

namespace DeezNuts.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetActiveProducts();
    }
}
using DeezNuts.Repositories;

namespace DeezNuts.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public string BuildProductList()
        {
            var products = _repository.GetActiveProducts();
            var returnString = "";

            foreach (var product in products)
            {
                returnString += product.Name + " - " + product.Description + System.Environment.NewLine;
            }

            return returnString.Trim();
        }
    }
}

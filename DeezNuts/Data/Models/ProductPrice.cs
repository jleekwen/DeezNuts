using DeezNuts.Enums;

namespace DeezNuts.Data.Models
{
    public class ProductPrice : BaseModel
    {
        public QuantityType QuantityType { get; set; }
        public float Price { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}

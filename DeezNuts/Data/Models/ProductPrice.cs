using DeezNuts.Enums;

namespace DeezNuts.Data.Models
{
    public class ProductPrice : BaseModel
    {
        int ProductId { get; set; }
        Product Product { get; set; }
        QuantityType Type { get; set; }
        float Price { get; set; }
    }

}

using DeezNuts.Data.Models;

namespace DeezNuts.Data.Models
{
    public class OrderItem : BaseModel
    {
        int OrderId { get; set; }
        Order Order { get; set; }
        int ProductId { get; set; }
        Product Product { get; set; }
        int Quantity { get; set; }
        float PriceTotal { get; set; }
    }
}

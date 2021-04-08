using System.Collections.Generic;

namespace DeezNuts.Data.Models
{
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ProductPrice> Prices { get; set; }
        public bool IsActive { get; set; }
    }
}

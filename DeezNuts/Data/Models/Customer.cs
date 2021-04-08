using System.Collections.Generic;

namespace DeezNuts.Data.Models
{
    public class Customer : BaseModel
    {
        public string? Name { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<CustomerAddress> Addresses { get; set; }
        public ICollection<Order> Orders { get; set; }
        public bool IsActive { get; set; }
        public virtual CustomerSession? Session { get; set; }

        public Customer() : base()
        {
            IsActive = true;
            Session = new CustomerSession();
        }
    }
}

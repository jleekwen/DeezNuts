using DeezNuts.Enums;
using System;
using System.Collections.Generic;

namespace DeezNuts.Data.Models
{
    public class Order : BaseModel
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderItem> Items { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime StatusDateTime { get; set; }
        public DateTime? ExpectedDeliveryDateTime { get; set; }

        public Order() : base()
        {
            this.Status = OrderStatus.New;
            this.StatusDateTime = DateTime.Now;
        }
    }
}

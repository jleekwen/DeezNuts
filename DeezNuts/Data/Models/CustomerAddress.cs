namespace DeezNuts.Data.Models
{
    public class CustomerAddress : BaseModel
    {
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
    }
}

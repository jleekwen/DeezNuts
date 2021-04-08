using System;

namespace DeezNuts.Data.Models
{
    public class MessageLog : BaseModel
    {
        //public int? CustomerId { get; set; }
        //public Customer? Customer { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
        public string TwilioMessageSid { get; set; }
        public string? Status { get; set; }
    }
}

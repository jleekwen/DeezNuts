using DeezNuts.Enums;
using System;

namespace DeezNuts.Data.Models
{
    public class CustomerSession : BaseModel
    {
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public string ToNumber { get; set; }
        public DateTime LastActivityDateTime { get; set; }
        public SessionState AwaitState { get; set; }
        public string Vars { get; set; }

        public CustomerSession(SessionState awaitState = SessionState.GreetingNew) : base()
        {
            LastActivityDateTime = DateTime.Now;
            AwaitState = awaitState;
        }
    }
}

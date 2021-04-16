namespace DeezNuts.Enums
{
    public enum SessionState
    {
        //awaiting response states
        AwaitAddress,
        AwaitName,
        Listening,
        Order,

        //new session
        GreetingNew,
        GreetingReturning
    }
}

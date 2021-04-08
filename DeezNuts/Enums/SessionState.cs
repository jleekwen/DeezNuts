namespace DeezNuts.Enums
{
    public enum SessionState
    {
        //awaiting response states
        AwaitAddress,
        AwaitName,
        Listening,

        //new session
        GreetingNew,
        GreetingReturning
    }
}

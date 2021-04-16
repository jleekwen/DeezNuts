namespace DeezNuts.Enums
{
    public enum MessageType
    {
        IntroGreetingNew,
        IntroGreetingReturning,

        OrderResponse,

        RequestName,
        RequestNameResponseFail,
        RequestNameResponseSuccess,

        ListeningActionResponseNoMatch,
        ListeningActionResponseMultipleMatches,

        Error,
        Help,
        ProductsList,
        Schedule
    }
}

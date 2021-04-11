namespace DeezNuts.Enums
{
    public enum MessageType
    {
        IntroGreetingNew,
        IntroGreetingReturning,

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

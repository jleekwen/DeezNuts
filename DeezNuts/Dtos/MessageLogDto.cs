using Twilio.AspNet.Common;

namespace DeezNuts.Dtos
{
    public class MessageLogDto
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Message { get; set; }
        public string TwilioMessageSid { get; set; }
        public string Status { get; set; }

        public MessageLogDto()
        { }

        public MessageLogDto(SmsRequest request)
        {
            From = request.From;
            To = request.To;
            Message = request.Body;
            TwilioMessageSid = request.SmsSid;
            Status = "received";
        }

        public MessageLogDto(SmsStatusCallbackRequest request)
        {
            From = request.From;
            To = request.To;
            Message = request.Body;
            TwilioMessageSid = request.SmsSid;
            Status = request.MessageStatus;
        }
    }
}

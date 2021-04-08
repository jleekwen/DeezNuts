//using Microsoft.Extensions.Hosting;

namespace DeezNuts
{
    public class DeezNutsConfig
    {
        public string ConnectionString { get; set; }
        public string TwilioAuth { get; set; }
        public string TwilioSid { get; set; }
        public string GoogleCredentialFile { get; set; }
    }
}

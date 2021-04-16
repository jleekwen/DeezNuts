using Google.Cloud.Language.V1;
using Microsoft.Extensions.Options;

namespace DeezNuts.Services
{
    public class GoogleAnalyticsService : IGoogleAnalyticsService
    {
        private readonly DeezNutsConfig _config;

        public GoogleAnalyticsService(
            IOptions<DeezNutsConfig> config)
        {
            _config = config.Value;
        }

        public AnalyzeEntitiesResponse Analyze(string input)
        {
            LanguageServiceClientBuilder builder = new LanguageServiceClientBuilder
            {
                CredentialsPath = _config.GoogleCredentialFile
            };

            var languageServiceClient = builder.Build();

            var document = Document.FromPlainText($"{input}");
            return languageServiceClient.AnalyzeEntities(document);
        }
    }
}

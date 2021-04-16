using Google.Cloud.Language.V1;

namespace DeezNuts.Services
{
    public interface IGoogleAnalyticsService
    {
        AnalyzeEntitiesResponse Analyze(string input);
    }
}
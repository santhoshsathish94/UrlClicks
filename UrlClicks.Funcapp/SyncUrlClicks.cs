using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using UrlClicks.Application.Interface;

namespace UrlClicks.Funcapp
{
    public class SyncUrlClicks
    {
        private IAppInsightsService _appInsightsService;        
        public SyncUrlClicks(IAppInsightsService appInsightsService)
        {
            _appInsightsService = appInsightsService;
        }

        [FunctionName("SyncUrlClicks")]
        public void Run([TimerTrigger("0 0 */1 * * *", RunOnStartup = true)]TimerInfo myTimer, ILogger log)
        {
            try
            {
                _appInsightsService.SyncUrlClicksAsync(DateTime.Now).GetAwaiter();
                log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            }
            catch (Exception ex)
            {
                log.LogError(ex, $"C# Timer trigger function failed at: {DateTime.Now}");
            }            
        }
    }
}

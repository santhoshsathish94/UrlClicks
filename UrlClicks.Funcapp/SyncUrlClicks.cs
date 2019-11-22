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
        public void Run([TimerTrigger("0 * */1 * * *")]TimerInfo myTimer, ILogger log)
        {
            _appInsightsService.SyncUrlClicksAsync(DateTime.Now).GetAwaiter();
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}

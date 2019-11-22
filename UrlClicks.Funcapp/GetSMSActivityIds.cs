using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using UrlClicks.Application.Interface;
using UrlClicks.Infrastructure.Models;

namespace UrlClicks.Funcapp
{
    public class GetSMSActivityIds
    {
        private IAppInsightsService _appInsightsService;
        public GetSMSActivityIds(IAppInsightsService appInsightsService)
        {
            _appInsightsService = appInsightsService;
        }

        [FunctionName("GetSMSActivityIds")]
        public void Run([QueueTrigger("1deletethisqueue", Connection = "AzureWebJobsStorage")]string myQueueItem, ILogger log)
        {
            var activityModel = JsonConvert.DeserializeObject<List<ActivityQueueModel>>(myQueueItem);
            _appInsightsService.SyncActivityIds(activityModel.FirstOrDefault()).GetAwaiter();
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}

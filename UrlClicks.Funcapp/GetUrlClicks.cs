using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using UrlClicks.Infrastructure.Interface;
using UrlClicks.Persistence.Interface;

namespace UrlClicks.Funcapp
{
    public class GetUrlClicks
    {        
        private IAppInsightsRepository _appInsightsRepository;
        private IUnitOfWork _uow;
        private IAzureStorageRepository _azureStorageRepository;
        private string apikey = Environment.GetEnvironmentVariable("AIAPIKey");
        public GetUrlClicks(IAppInsightsRepository appInsightsRepository, IUnitOfWork uow, IAzureStorageRepository azureStorageRepository)
        {            
            _appInsightsRepository = appInsightsRepository;
            _azureStorageRepository = azureStorageRepository;
            _uow = uow;
            
        }        

        [FunctionName("GetUrlClicks")]
        public void Run([QueueTrigger("1deletethisqueue", Connection = "AzureWebJobsStorage")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}

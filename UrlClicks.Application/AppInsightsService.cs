using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using UrlClicks.Domain.Models;
using UrlClicks.Infrastructure.Interface;
using UrlClicks.Persistence.Interface;

namespace UrlClicks.Application
{
    public class AppInsightsService
    {                
        private IAppInsightsRepository _appInsightsRepository;
        private IUnitOfWork _uow;
        private ILogger<AppInsightsService> _logger { get; }
        private IAzureStorageRepository _azureStorageRepository;

        public AppInsightsService(IAppInsightsRepository appInsightsRepository,
                                  IUnitOfWork uow,
                                  ILogger<AppInsightsService> logger,
                                  IAzureStorageRepository azureStorageRepository)
        {                        
            _appInsightsRepository = appInsightsRepository;
            _uow = uow;
            _logger = logger;
            _azureStorageRepository = azureStorageRepository;
        }
        
        public async Task SyncUrlClicks(DateTime date)
        {
            var urlclicks = await _appInsightsRepository.GetUrlClick(date,
                                                               "b762e2de-21d1-4176-a9a5-f0921247fd41",
                                                               "ovvv6cgzzzvzt87y480teiubixmk0r5bobibycy8");
            _uow.UrlClickRepo.Merge(urlclicks);

            var moduleClicks = urlclicks.GroupBy(c => new { c.ModuleClickId, c.Date }).Select(c => new ModuleClick
            {
                Id = c.Key.ModuleClickId,
                Date = c.Key.Date,
                UniqueClicks = c.Count(),
                TotalClicks = c.Sum(b => b.Count),
                LastModifiedDate = c.Max(b => b.LastModifiedDate)
            }).ToList();

            var data = urlclicks.GroupBy(c=> c.ModuleClickId).Select(c=> )
            await _azureStorageRepository.AddQueueAsync();
        }
    }
}

using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using UrlClicks.Domain.Models;
using UrlClicks.Infrastructure.Interface;
using UrlClicks.Persistence.Interface;
using UrlClicks.Application.Interface;
using UrlClicks.Infrastructure.Models;
using System.Net.Http;
using System.Text;
using System.Collections.Generic;
using UrlClicks.Domain.Models.SMS;
using UrlClicks.Application.Models;

namespace UrlClicks.Application.Implemention
{
    public class AppInsightsService : IAppInsightsService
    {                
        private IAppInsightsRepository _appInsightsRepository;
        private IUnitOfWork _uow;
        private ILogger<AppInsightsService> _logger;
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
        
        public async Task SyncUrlClicksAsync(DateTime date)
        {
            var urlclicks = await _appInsightsRepository.GetUrlClick(date,
                                                               "b762e2de-21d1-4176-a9a5-f0921247fd41",
                                                               "ovvv6cgzzzvzt87y480teiubixmk0r5bobibycy8");
            var moduleClicks = urlclicks.GroupBy(c => new { c.ModuleClickId, c.Date }).Select(c => new ModuleClick
            {
                Id = c.Key.ModuleClickId,
                Date = c.Key.Date,
                UniqueClicks = c.Count(),
                TotalClicks = c.Sum(b => b.Count),
                LastModifiedDate = c.Max(b => b.LastModifiedDate)
            }).ToList();            

            var data = urlclicks.GroupBy(c => c.ModuleClickId).Select(c => new { ModuleClickId = c.Key, UrlClickIds = c.Select(j=>j.Id)});
            
            foreach(var item in moduleClicks)
            {
                var activityModel = urlclicks.Where(c => c.ModuleClickId == item.Id).GroupBy(c => c.ModuleClickId).Select(c => new ActivityQueueModel { 
                    ModuleClickId = c.Key,
                    ClickIds = c.Select(s=>s.Id).ToList()
                });
                await _azureStorageRepository.AddQueueAsync(JsonConvert.SerializeObject(activityModel),"1deletethisqueue");
            }

            _uow.UrlClickRepo.Merge(urlclicks);
            _uow.ModuleClickRepo.Merge(moduleClicks);
            _uow.Save();            
        }

        public async Task SyncActivityIds(ActivityQueueModel activityModel)
        {
            var activites = new List<ActivityResponseModel>();
            using (var httpClient = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(activityModel), Encoding.UTF8, "application/json");
                using (var response = httpClient.PostAsync("https://messageservicestg.wechatify.com/api/SMS/GetActivityIds", content).Result)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var res = JsonConvert.DeserializeObject<APIResponseModel>(result);
                    activites = JsonConvert.DeserializeObject<List<ActivityResponseModel>>(res.Result.ToString());
                }
            }
            var activityTracks = from a in activites
                                select new LinkSmsActivity
                                {
                                    UrlClickId = a.ClickId,
                                    SmsActivityClickId = a.ActivityId
                                };
            _uow.LinkSmsActivityRepo.Merge(activityTracks);
            _uow.Save();
        }
    }
}

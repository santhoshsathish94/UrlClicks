using Newtonsoft.Json;
using Pathoschild.Http.Client;
using Pathoschild.Http.Client.Extensibility;
using Pathoschild.Http.Client.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using UrlClicks.Domain.Enums;
using UrlClicks.Domain.Models;
using UrlClicks.Infrastructure.Interface;
using UrlClicks.Infrastructure.Models.AppInsights;

namespace UrlClicks.Infrastructure.Implemention
{
    public class AppInsightsRepository : ApiRepository, IAppInsightsRepository
    {        
        public AppInsightsRepository(HttpClient httpClient) : base(httpClient)
        {
            
        }

        public async Task<IEnumerable<UrlClick>> GetUrlClick(DateTime date, string appId, string apikey)
        {
            var urlTracks = new List<UrlClick>();

            var dataList = new AppInsightResponse();
            //var dataList = await _httpClient
            //    .Fluent()
            //    .GetAsync($"{appId}/query")                
            //    .WithHeader("x-api-key", apikey)
            //    .WithArguments(new
            //    {
            //        timespan = date.ToString("yyyy-MM-dd") + "%2F" + date.AddDays(1).ToString("yyyy-MM-dd"),
            //        query = "urlclicks"
            //    }).As<AppInsightResponse>();

            _httpClient.DefaultRequestHeaders.Add("x-api-key", apikey);
            using (var response = await _httpClient.GetAsync("https://api.applicationinsights.io/v1/apps/b762e2de-21d1-4176-a9a5-f0921247fd41/query?timespan="+
                date.ToString("yyyy-MM-dd") + "%2F" + date.AddDays(1).ToString("yyyy-MM-dd") + "&query=urlclicks"))
            {
                var result = await response.Content.ReadAsStringAsync();
                dataList = JsonConvert.DeserializeObject<AppInsightResponse>(result);
            }

            foreach (var item in dataList.tables.FirstOrDefault().rows)
            {
                Guid trackId;
                Guid moduleClickId;
                if (Guid.TryParse((string)item[0], out trackId) && Guid.TryParse((string)item[3], out moduleClickId))
                    urlTracks.Add(new UrlClick
                    {
                        Id = trackId,
                        Date = (DateTime)item[1],
                        Type = (ModuleType)Convert.ToInt32(item[2]),
                        ModuleClickId = moduleClickId,
                        Url = (string)item[4],
                        Count = (int)(long)item[5],
                        LastModifiedDate = (DateTime)item[6],
                    });
            }
            return urlTracks;
        }
    }
}

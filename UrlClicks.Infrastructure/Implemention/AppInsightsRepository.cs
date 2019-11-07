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

            var dataList = await _httpClient
                .Fluent()
                .GetAsync($"{appId}/query")
                .WithHeader("Content-Type", "application/json")
                .WithHeader("x-api-key", apikey)
                .WithArguments(new
                {
                    timespan = date.AddDays(-1).ToString("yyyy-MM-dd") + "%2F" + date.ToString("yyyy-MM-dd"),
                    query = "urlclicks"
                }).As<AppInsightResponse>();

            foreach (var item in dataList.tables.FirstOrDefault().rows)
            {
                if (item[0] is Guid)
                    urlTracks.Add(new UrlClick{
                        Id = (Guid)item[0],
                        Date = (DateTime)item[1],
                        Type = (ModuleType)item[2],
                        ModuleClickId = (Guid)item[3],
                        Url = (string)item[4],
                        Count = (int)(long)item[5],
                        LastModifiedDate = (DateTime)item[6],
                    });
            }
            return urlTracks;
        }
    }
}

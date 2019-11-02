using Pathoschild.Http.Client;
using Pathoschild.Http.Client.Extensibility;
using Pathoschild.Http.Client.Retry;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using UrlClicks.Infrastructure.Interface;

namespace UrlClicks.Infrastructure.Implemention
{
    public class AppInsightsRepository : ApiRepository, IAppInsightsRepository
    {        
        public AppInsightsRepository(HttpClient httpClient) : base(httpClient)
        {
            
        }        
    }
}

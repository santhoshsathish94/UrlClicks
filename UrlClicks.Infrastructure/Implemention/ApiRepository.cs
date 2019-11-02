using Pathoschild.Http.Client;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using UrlClicks.Infrastructure.Interface;

namespace UrlClicks.Infrastructure.Implemention
{
    public class ApiRepository : IApiRepository
    {
        protected readonly HttpClient _httpClient;

        public ApiRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public IClient Api()
        {
            return new FluentClient(_httpClient.BaseAddress, _httpClient);
        }
    }
}

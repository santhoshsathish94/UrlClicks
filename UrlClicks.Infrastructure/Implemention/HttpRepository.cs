using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Pathoschild.Http.Client;
using UrlClicks.Infrastructure.Interface;

namespace UrlClicks.Infrastructure.Implemention
{
    public class HttpRepository : IHttpRepository
    {
        protected readonly HttpClient _httpClient;
        
        public HttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }        

        public IClient Uri(Uri uri)
        {
            return new FluentClient(uri, _httpClient);
        }

        public IClient Url(string url)
        {
            return new FluentClient(url, _httpClient);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Pathoschild.Http.Client
{
    public static class Extensions
    {
        public static IClient ToHttpClient(this string url)
        {
            return new FluentClient(url);
        }
        public static IClient ToHttpClient(this Uri uri)
        {
            return new FluentClient(uri);
        }
        public static IClient Fluent(this HttpClient httpClient)
        {
            return new FluentClient(httpClient.BaseAddress,httpClient);
        }
    }
}

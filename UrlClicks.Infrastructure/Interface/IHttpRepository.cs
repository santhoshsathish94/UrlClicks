using Pathoschild.Http.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace UrlClicks.Infrastructure.Interface
{
    public interface IHttpRepository 
    {        
        IClient Url(string url);
        IClient Uri(Uri uri);
    }
}

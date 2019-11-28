using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace UrlClicks.Infrastructure.Common
{
    public static class AppSettings
    {
        public static readonly IConfiguration Config;

        static AppSettings()
        {
            Config = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        }

        public static string GetValue(string key)
        {
            return Config[key] ?? Environment.GetEnvironmentVariable(key);
        }
    }
}

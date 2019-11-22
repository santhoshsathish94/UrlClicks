using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using UrlClicks.Application.Implemention;
using UrlClicks.Infrastructure.Implemention;
using UrlClicks.Infrastructure.Interface;
using UrlClicks.Persistence.Implemention;
using UrlClicks.Persistence.Interface;
using UrlClicks.Application.Interface;

[assembly: FunctionsStartup(typeof(UrlClicks.Funcapp.Startup))]

namespace UrlClicks.Funcapp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var connectionString = Environment.GetEnvironmentVariable("UrlClickDbConnection");
            builder.Services.AddPersistance(connectionString);
            builder.Services.AddHttpClient<IHttpRepository, HttpRepository>();

            var AppInsightsRestUrl = Environment.GetEnvironmentVariable("AppInsightsRestUrl");
            builder.Services.AddHttpClient<IAppInsightsRepository, AppInsightsRepository>(
                client => client.BaseAddress = new Uri(AppInsightsRestUrl)
                );

            var AzureWebJobsStorage = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
            builder.Services.AddTransient<IAzureStorageRepository, AzureStorageRepository>(c =>
            {
                return new AzureStorageRepository(AzureWebJobsStorage);
            });
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IAppInsightsService, AppInsightsService>();
        }
    }
}

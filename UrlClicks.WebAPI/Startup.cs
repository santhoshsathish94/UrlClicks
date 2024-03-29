﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UrlClicks.Infrastructure.Implemention;
using UrlClicks.Infrastructure.Interface;
using UrlClicks.Persistence;

namespace UrlClicks.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddApplicationInsightsTelemetry();            
            var connectionString = Configuration.GetConnectionString("UrlClickDbConnection");
            services.AddPersistance(connectionString);
            services.AddHttpClient<IHttpRepository, HttpRepository>();
            var AppInsightsRestUrl = Configuration.GetValue<Uri>("AppInsightsRestUrl");
            services.AddHttpClient<IAppInsightsRepository, AppInsightsRepository>(
                client => client.BaseAddress = AppInsightsRestUrl
                );            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggingBuilder builder)
        {
            builder.AddConfiguration(Configuration);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();        
                builder.AddConsole();
                builder.AddDebug();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

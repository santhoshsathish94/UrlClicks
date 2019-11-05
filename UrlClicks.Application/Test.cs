using Microsoft.Extensions.Logging;
using Pathoschild.Http.Client;
using System;
using UrlClicks.Infrastructure.Interface;
using UrlClicks.Persistence.Interface;

namespace UrlClicks.Application
{
    public class Test
    {        
        private IHttpRepository _httpRepository;
        private IAppInsightsRepository _appInsightsRepository;
        private IUnitOfWork _uow;
        private ILogger<Test> _logger { get; }
        public Test(IHttpRepository httpRepository,IAppInsightsRepository appInsightsRepository,IUnitOfWork uow,ILogger<Test> logger)
        {            
            _httpRepository = httpRepository;
            _appInsightsRepository = appInsightsRepository;
            _uow = uow;
            _logger = logger;
        }
        
    }
}

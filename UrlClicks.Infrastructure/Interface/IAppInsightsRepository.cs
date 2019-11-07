using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrlClicks.Domain.Models;

namespace UrlClicks.Infrastructure.Interface
{
    public interface IAppInsightsRepository : IApiRepository
    {
        Task<IEnumerable<UrlClick>> GetUrlClick(DateTime date, string appId, string apikey);
    }
}

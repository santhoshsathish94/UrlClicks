using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UrlClicks.Infrastructure.Models;

namespace UrlClicks.Application.Interface
{
    public interface IAppInsightsService
    {
        Task SyncUrlClicksAsync(DateTime date);
        Task SyncActivityIds(ActivityQueueModel activityModel);
    }
}

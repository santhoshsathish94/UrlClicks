using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UrlClicks.Application.Interface
{
    public interface IAppInsightsService
    {
        Task SyncUrlClicksAsync(DateTime date);
    }
}

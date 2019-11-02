using System;
using System.Collections.Generic;
using System.Text;
using UrlClicks.Domain.Models;
using UrlClicks.Domain.Models.SMS;

namespace UrlClicks.Persistence.Interface
{
    public interface IUnitOfWork
    {
        IRepository<UrlClick> UrlClickRepo { get; }
        IRepository<ModuleClick> ModuleClickRepo { get; }
        IRepository<LinkSmsActivity> LinkSmsActivityRepo { get; }
        IRepository<SmsActivityClick> SmsActivityClickRepo { get; }
        void Save();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using UrlClicks.Domain.Models;
using UrlClicks.Domain.Models.SMS;
using UrlClicks.Persistence.Interface;

namespace UrlClicks.Persistence.Implemention
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UrlClickDbContext _urlClickDbContext;
        private IRepository<UrlClick> _urlClickRepo;
        private IRepository<ModuleClick> _moduleClickRepo;
        private IRepository<LinkSmsActivity> _linkSmsActivityRepo;
        private IRepository<SmsActivityClick> _smsActivityClickRepo;
        public UnitOfWork(UrlClickDbContext urlClickDbContext)
        {
            _urlClickDbContext = urlClickDbContext;
        }        

        public IRepository<UrlClick> UrlClickRepo
        {
            get
            {
                return _urlClickRepo = _urlClickRepo ?? new Repository<UrlClick>(_urlClickDbContext);
            }
        }

        public IRepository<ModuleClick> ModuleClickRepo
        {
            get
            {
                return _moduleClickRepo = _moduleClickRepo ?? new Repository<ModuleClick>(_urlClickDbContext);
            }
        }

        public IRepository<LinkSmsActivity> LinkSmsActivityRepo
        {
            get
            {
                return _linkSmsActivityRepo = _linkSmsActivityRepo ?? new Repository<LinkSmsActivity>(_urlClickDbContext);
            }
        }

        public IRepository<SmsActivityClick> SmsActivityClickRepo
        {
            get
            {
                return _smsActivityClickRepo = _smsActivityClickRepo ?? new Repository<SmsActivityClick>(_urlClickDbContext);
            }
        }

        public void Save()
        {
            _urlClickDbContext.SaveChanges();
        }
    }
}

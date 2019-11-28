using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using UrlClicks.Domain.Models;
using UrlClicks.Domain.Models.SMS;
using UrlClicks.Persistence.Configs;

namespace UrlClicks.Persistence
{
    public class UrlClickDbContext : DbContext
    {
        public DbSet<UrlClick> UrlClicks { get; set; }
        public DbSet<ModuleClick> ModuleClicks { get; set; }
        public DbSet<LinkSmsActivity> LinkSmsActivitys { get; set; }
        public DbSet<SmsActivityClick> SmsActivityClicks { get; set; }
        public UrlClickDbContext(DbContextOptions<UrlClickDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UrlClicksConfig());
            builder.ApplyConfiguration(new ModuleClicksConfig());
            builder.ApplyConfiguration(new LinkSmsActivitiesConfig());
            builder.ApplyConfiguration(new SmsActivityClicksConfig());
        }        
    }    
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UrlClicks.Domain.Models.SMS;

namespace UrlClicks.Persistence.Configs
{
    class LinkSmsActivitiesConfig : IEntityTypeConfiguration<LinkSmsActivity>
    {
        public void Configure(EntityTypeBuilder<LinkSmsActivity> builder)
        {
            builder.ToTable("LinkSmsActivities");            
            builder.HasKey(e => e.UrlClickId);
            builder.HasIndex(e => e.SmsActivityClickId);
        }
    }
}

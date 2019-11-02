using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UrlClicks.Domain.Models.SMS;

namespace UrlClicks.Persistence.Configs
{
    class SmsActivityClicksConfig : IEntityTypeConfiguration<SmsActivityClick>
    {
        public void Configure(EntityTypeBuilder<SmsActivityClick> builder)
        {
            builder.ToTable("SmsActivityClicks");
            builder.Property(e => e.Date).HasColumnType("Date");
            builder.HasKey(e => new { e.Id, e.Date });
            builder.Property(e => e.Urls)
            .HasConversion(
                v => string.Join(",", v),
                v => v.Split(','));
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UrlClicks.Domain.Models;

namespace UrlClicks.Persistence.Configs
{
    class UrlClicksConfig : IEntityTypeConfiguration<UrlClick>
    {
        public void Configure(EntityTypeBuilder<UrlClick> builder)
        {
            builder.ToTable("UrlClicks");
            builder.Property(e => e.Date).HasColumnType("Date");
            builder.HasKey(e => new { e.Id, e.Date });
            builder.HasIndex(e => e.ModuleClickId);
            builder.HasIndex(e => e.Url);
        }
    }
}

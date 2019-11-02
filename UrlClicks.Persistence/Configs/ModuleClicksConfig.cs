using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UrlClicks.Domain.Models;

namespace UrlClicks.Persistence.Configs
{
    class ModuleClicksConfig : IEntityTypeConfiguration<ModuleClick>
    {
        public void Configure(EntityTypeBuilder<ModuleClick> builder)
        {
            builder.ToTable("ModuleClicks");
            builder.Property(e => e.Date).HasColumnType("Date");
            builder.HasKey(e => new { e.Id, e.Date });            
        }
    }
}

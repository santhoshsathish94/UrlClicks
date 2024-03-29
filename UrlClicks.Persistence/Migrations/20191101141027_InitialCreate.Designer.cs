﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UrlClicks.Persistence;

namespace UrlClicks.Persistence.Migrations
{
    [DbContext(typeof(UrlClickDbContext))]
    [Migration("20191101141027_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UrlClicks.Domain.Models.ModuleClick", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<int>("TotalClicks");

                    b.Property<int>("UniqueClicks");

                    b.HasKey("Id", "Date");

                    b.ToTable("ModuleClicks");
                });

            modelBuilder.Entity("UrlClicks.Domain.Models.SMS.LinkSmsActivity", b =>
                {
                    b.Property<Guid>("UrlClickId")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("SmsActivityClickId");

                    b.HasKey("UrlClickId");

                    b.HasIndex("SmsActivityClickId");

                    b.ToTable("LinkSmsActivities");
                });

            modelBuilder.Entity("UrlClicks.Domain.Models.SMS.SmsActivityClick", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<int>("TotalClicks");

                    b.Property<int>("UniqueClicks");

                    b.Property<string>("Urls");

                    b.HasKey("Id", "Date");

                    b.ToTable("SmsActivityClicks");
                });

            modelBuilder.Entity("UrlClicks.Domain.Models.UrlClick", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<int>("Count");

                    b.Property<DateTime>("LastModifiedDate");

                    b.Property<Guid>("ModuleClickId");

                    b.Property<int>("Type");

                    b.Property<string>("Url");

                    b.HasKey("Id", "Date");

                    b.HasIndex("ModuleClickId");

                    b.HasIndex("Url");

                    b.ToTable("UrlClicks");
                });
#pragma warning restore 612, 618
        }
    }
}

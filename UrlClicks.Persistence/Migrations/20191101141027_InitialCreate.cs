using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UrlClicks.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinkSmsActivities",
                columns: table => new
                {
                    UrlClickId = table.Column<Guid>(nullable: false),
                    SmsActivityClickId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkSmsActivities", x => x.UrlClickId);
                });

            migrationBuilder.CreateTable(
                name: "ModuleClicks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    UniqueClicks = table.Column<int>(nullable: false),
                    TotalClicks = table.Column<int>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModuleClicks", x => new { x.Id, x.Date });
                });

            migrationBuilder.CreateTable(
                name: "SmsActivityClicks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    Urls = table.Column<string>(nullable: true),
                    UniqueClicks = table.Column<int>(nullable: false),
                    TotalClicks = table.Column<int>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmsActivityClicks", x => new { x.Id, x.Date });
                });

            migrationBuilder.CreateTable(
                name: "UrlClicks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(type: "Date", nullable: false),
                    Type = table.Column<int>(nullable: false),
                    ModuleClickId = table.Column<Guid>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlClicks", x => new { x.Id, x.Date });
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkSmsActivities_SmsActivityClickId",
                table: "LinkSmsActivities",
                column: "SmsActivityClickId");

            migrationBuilder.CreateIndex(
                name: "IX_UrlClicks_ModuleClickId",
                table: "UrlClicks",
                column: "ModuleClickId");

            migrationBuilder.CreateIndex(
                name: "IX_UrlClicks_Url",
                table: "UrlClicks",
                column: "Url");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LinkSmsActivities");

            migrationBuilder.DropTable(
                name: "ModuleClicks");

            migrationBuilder.DropTable(
                name: "SmsActivityClicks");

            migrationBuilder.DropTable(
                name: "UrlClicks");
        }
    }
}

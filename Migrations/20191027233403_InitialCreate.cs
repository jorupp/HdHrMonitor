using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HdHrMonitor.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Data",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateTimeUtc = table.Column<DateTimeOffset>(nullable: false),
                    TunerNumber = table.Column<int>(nullable: false),
                    Channel = table.Column<string>(nullable: true),
                    ChannelFrequency = table.Column<string>(nullable: true),
                    ProgramNumber = table.Column<string>(nullable: true),
                    Authorization = table.Column<string>(nullable: true),
                    CCIProtection = table.Column<string>(nullable: true),
                    ModulationLock = table.Column<string>(nullable: true),
                    PCRLock = table.Column<string>(nullable: true),
                    SignalStrengthPct = table.Column<int>(nullable: false),
                    SignalStrengthDbm = table.Column<int>(nullable: false),
                    SignalQualityPct = table.Column<int>(nullable: false),
                    SignalQualityDbm = table.Column<int>(nullable: false),
                    SymbolQualityPct = table.Column<int>(nullable: false),
                    StreamingRateRaw = table.Column<string>(nullable: true),
                    ResourceLock = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Data", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Data");
        }
    }
}

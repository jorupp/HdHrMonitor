using Microsoft.EntityFrameworkCore.Migrations;

namespace HdHrMonitor.Migrations
{
    public partial class AddNumberFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SignalQualityDbm",
                table: "Data",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SignalQualityPct",
                table: "Data",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SignalStrengthDbm",
                table: "Data",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SignalStrengthPct",
                table: "Data",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SymbolQualityPct",
                table: "Data",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SignalQualityDbm",
                table: "Data");

            migrationBuilder.DropColumn(
                name: "SignalQualityPct",
                table: "Data");

            migrationBuilder.DropColumn(
                name: "SignalStrengthDbm",
                table: "Data");

            migrationBuilder.DropColumn(
                name: "SignalStrengthPct",
                table: "Data");

            migrationBuilder.DropColumn(
                name: "SymbolQualityPct",
                table: "Data");
        }
    }
}

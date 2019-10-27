using Microsoft.EntityFrameworkCore.Migrations;

namespace HdHrMonitor.Migrations
{
    public partial class UseStringsForNow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "SignalQuality",
                table: "Data",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignalStrength",
                table: "Data",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SymbolQuality",
                table: "Data",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SignalQuality",
                table: "Data");

            migrationBuilder.DropColumn(
                name: "SignalStrength",
                table: "Data");

            migrationBuilder.DropColumn(
                name: "SymbolQuality",
                table: "Data");

            migrationBuilder.AddColumn<int>(
                name: "SignalQualityDbm",
                table: "Data",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SignalQualityPct",
                table: "Data",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SignalStrengthDbm",
                table: "Data",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SignalStrengthPct",
                table: "Data",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SymbolQualityPct",
                table: "Data",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

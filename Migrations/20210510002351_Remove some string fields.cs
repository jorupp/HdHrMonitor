using Microsoft.EntityFrameworkCore.Migrations;

namespace HdHrMonitor.Migrations
{
    public partial class Removesomestringfields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SignalQuality",
                table: "Data",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SignalStrength",
                table: "Data",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SymbolQuality",
                table: "Data",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

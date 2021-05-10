using Microsoft.EntityFrameworkCore.Migrations;

namespace HdHrMonitor.Migrations
{
    public partial class Addenums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorizationEnum",
                table: "Data",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CCIProtectionEnum",
                table: "Data",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PCRLockEnum",
                table: "Data",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorizationEnum",
                table: "Data");

            migrationBuilder.DropColumn(
                name: "CCIProtectionEnum",
                table: "Data");

            migrationBuilder.DropColumn(
                name: "PCRLockEnum",
                table: "Data");
        }
    }
}

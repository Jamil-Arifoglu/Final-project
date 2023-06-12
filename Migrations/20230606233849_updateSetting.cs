using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gaming.Migrations
{
    public partial class updateSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "key",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "value",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "key",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "value",
                table: "Settings");
        }
    }
}

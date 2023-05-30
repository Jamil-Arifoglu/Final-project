using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gaming.Migrations
{
    public partial class createTableColorSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "GamingSize");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "GamingColor");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sizes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quantity",
                table: "Sizes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Colors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quantity",
                table: "Colors",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Sizes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Colors");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Colors");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GamingSize",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GamingColor",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

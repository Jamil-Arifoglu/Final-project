using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gaming.Migrations
{
    public partial class updateColorSize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "GamingSize",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "GamingColor",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GamingColorId",
                table: "BasketItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GamingSizeId",
                table: "BasketItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_GamingColorId",
                table: "BasketItems",
                column: "GamingColorId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_GamingSizeId",
                table: "BasketItems",
                column: "GamingSizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_GamingColor_GamingColorId",
                table: "BasketItems",
                column: "GamingColorId",
                principalTable: "GamingColor",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketItems_GamingSize_GamingSizeId",
                table: "BasketItems",
                column: "GamingSizeId",
                principalTable: "GamingSize",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_GamingColor_GamingColorId",
                table: "BasketItems");

            migrationBuilder.DropForeignKey(
                name: "FK_BasketItems_GamingSize_GamingSizeId",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_GamingColorId",
                table: "BasketItems");

            migrationBuilder.DropIndex(
                name: "IX_BasketItems_GamingSizeId",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "GamingSize");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "GamingColor");

            migrationBuilder.DropColumn(
                name: "GamingColorId",
                table: "BasketItems");

            migrationBuilder.DropColumn(
                name: "GamingSizeId",
                table: "BasketItems");
        }
    }
}

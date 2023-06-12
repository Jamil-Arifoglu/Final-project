using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gaming.Migrations
{
    public partial class updateContacttable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Callus",
                table: "Contacts",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Contacts",
                newName: "LastName");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Contacts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Contacts",
                newName: "Callus");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Contacts",
                newName: "Address");
        }
    }
}

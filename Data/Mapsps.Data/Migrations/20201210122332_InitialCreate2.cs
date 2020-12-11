using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mapsps.Data.Migrations
{
    public partial class InitialCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserNicknames");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "UserNicknames");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserCats");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "UserCats");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserNicknames",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "UserNicknames",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserCats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "UserCats",
                type: "datetime2",
                nullable: true);
        }
    }
}

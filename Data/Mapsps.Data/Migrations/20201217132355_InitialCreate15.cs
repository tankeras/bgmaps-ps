using Microsoft.EntityFrameworkCore.Migrations;

namespace Mapsps.Data.Migrations
{
    public partial class InitialCreate15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Cats");

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "Cats",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Region",
                table: "Cats");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Cats",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

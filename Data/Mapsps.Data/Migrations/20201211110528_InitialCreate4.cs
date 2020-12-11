using Microsoft.EntityFrameworkCore.Migrations;

namespace Mapsps.Data.Migrations
{
    public partial class InitialCreate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Cats_CatId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CatId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CatId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CatId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CatId",
                table: "AspNetUsers",
                column: "CatId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Cats_CatId",
                table: "AspNetUsers",
                column: "CatId",
                principalTable: "Cats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

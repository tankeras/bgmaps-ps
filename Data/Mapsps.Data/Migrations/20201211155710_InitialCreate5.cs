using Microsoft.EntityFrameworkCore.Migrations;

namespace Mapsps.Data.Migrations
{
    public partial class InitialCreate5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Nicknames_NicknameId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NicknameId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NicknameId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Cats",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Cats");

            migrationBuilder.AddColumn<int>(
                name: "NicknameId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NicknameId",
                table: "AspNetUsers",
                column: "NicknameId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Nicknames_NicknameId",
                table: "AspNetUsers",
                column: "NicknameId",
                principalTable: "Nicknames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

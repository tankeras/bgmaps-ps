using Microsoft.EntityFrameworkCore.Migrations;

namespace Mapsps.Data.Migrations
{
    public partial class InitialCreate145 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CatId",
                table: "Nicknames",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CatId",
                table: "Nicknames",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}

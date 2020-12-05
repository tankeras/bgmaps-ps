using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Generic;

namespace Mapsps.Data.Migrations
{
    public partial class InitialCreate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MostVotedNickname",
                table: "Cats",
                nullable: false,
                defaultValue: 0.0);
          
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MostVotedNickname",
                table: "Cats");
        }
    }
}

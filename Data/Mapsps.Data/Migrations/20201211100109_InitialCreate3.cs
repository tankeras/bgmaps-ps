using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mapsps.Data.Migrations
{
    public partial class InitialCreate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCats");

            migrationBuilder.DropTable(
                name: "UserNicknames");

            migrationBuilder.CreateTable(
                name: "ConfirmedPets",
                columns: table => new
                {
                    CatId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfirmedPets", x => new { x.CatId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ConfirmedPets_Cats_CatId",
                        column: x => x.CatId,
                        principalTable: "Cats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ConfirmedPets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Upvotes",
                columns: table => new
                {
                    NicknameId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Upvotes", x => new { x.NicknameId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Upvotes_Nicknames_NicknameId",
                        column: x => x.NicknameId,
                        principalTable: "Nicknames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Upvotes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfirmedPets_UserId",
                table: "ConfirmedPets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Upvotes_UserId",
                table: "Upvotes",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfirmedPets");

            migrationBuilder.DropTable(
                name: "Upvotes");

            migrationBuilder.CreateTable(
                name: "UserCats",
                columns: table => new
                {
                    CatId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCats", x => new { x.CatId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserCats_Cats_CatId",
                        column: x => x.CatId,
                        principalTable: "Cats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserCats_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserNicknames",
                columns: table => new
                {
                    NicknameId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNicknames", x => new { x.NicknameId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserNicknames_Nicknames_NicknameId",
                        column: x => x.NicknameId,
                        principalTable: "Nicknames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserNicknames_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCats_UserId",
                table: "UserCats",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNicknames_UserId",
                table: "UserNicknames",
                column: "UserId");
        }
    }
}

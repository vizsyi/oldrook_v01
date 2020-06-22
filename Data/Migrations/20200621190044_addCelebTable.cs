using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Oldrook.Data.Migrations
{
    public partial class addCelebTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Celebs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fullname = table.Column<string>(maxLength: 48, nullable: false),
                    PId = table.Column<Guid>(nullable: false),
                    Sex = table.Column<byte>(nullable: false),
                    Ready = table.Column<bool>(nullable: false),
                    BestFriendId = table.Column<int>(nullable: true),
                    Image = table.Column<byte[]>(nullable: true),
                    ContentType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Celebs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Celebs_Celebs_BestFriendId",
                        column: x => x.BestFriendId,
                        principalTable: "Celebs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Celebs_BestFriendId",
                table: "Celebs",
                column: "BestFriendId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Celebs");
        }
    }
}

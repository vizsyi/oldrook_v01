using Microsoft.EntityFrameworkCore.Migrations;

namespace Oldrook.Data.Migrations
{
    public partial class addIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Celebs_PId",
                table: "Celebs",
                column: "PId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Celebs_PId",
                table: "Celebs");
        }
    }
}

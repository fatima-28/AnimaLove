using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimaLove.Migrations
{
    public partial class SomeChangesAboutPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_AppUserId",
                table: "Posts");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AppUserId",
                table: "Posts",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Posts_AppUserId",
                table: "Posts");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_AppUserId",
                table: "Posts",
                column: "AppUserId",
                unique: true,
                filter: "[AppUserId] IS NOT NULL");
        }
    }
}

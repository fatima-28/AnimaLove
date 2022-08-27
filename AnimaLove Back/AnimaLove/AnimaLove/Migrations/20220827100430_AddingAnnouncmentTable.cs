using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimaLove.Migrations
{
    public partial class AddingAnnouncmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Pets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_AppUserId",
                table: "Pets",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_AspNetUsers_AppUserId",
                table: "Pets",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_AspNetUsers_AppUserId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_AppUserId",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Pets");
        }
    }
}

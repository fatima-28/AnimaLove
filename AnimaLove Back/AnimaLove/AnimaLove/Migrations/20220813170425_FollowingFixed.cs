using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimaLove.Migrations
{
    public partial class FollowingFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FollowingUser",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FollowerUser",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FollowingUser");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FollowerUser");
        }
    }
}

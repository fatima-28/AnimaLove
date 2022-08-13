using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimaLove.Migrations
{
    public partial class UpdateingFollowerFollowingTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FollowingUser");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FollowerUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FollowingUser",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FollowerUser",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

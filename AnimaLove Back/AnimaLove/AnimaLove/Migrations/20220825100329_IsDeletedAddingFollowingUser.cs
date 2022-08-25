using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimaLove.Migrations
{
    public partial class IsDeletedAddingFollowingUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FollowingUser",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FollowingUser");
        }
    }
}

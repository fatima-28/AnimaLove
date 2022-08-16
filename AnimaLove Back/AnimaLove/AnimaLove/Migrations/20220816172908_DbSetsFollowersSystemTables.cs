using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimaLove.Migrations
{
    public partial class DbSetsFollowersSystemTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowerUser_Follower_FollowerId",
                table: "FollowerUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Follower",
                table: "Follower");

            migrationBuilder.RenameTable(
                name: "Follower",
                newName: "Followers");

            migrationBuilder.AlterColumn<string>(
                name: "FollowingId",
                table: "FollowingUser",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Followers",
                table: "Followers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Followings",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Followings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FollowingUser_FollowingId",
                table: "FollowingUser",
                column: "FollowingId");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowerUser_Followers_FollowerId",
                table: "FollowerUser",
                column: "FollowerId",
                principalTable: "Followers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowingUser_Followings_FollowingId",
                table: "FollowingUser",
                column: "FollowingId",
                principalTable: "Followings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FollowerUser_Followers_FollowerId",
                table: "FollowerUser");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowingUser_Followings_FollowingId",
                table: "FollowingUser");

            migrationBuilder.DropTable(
                name: "Followings");

            migrationBuilder.DropIndex(
                name: "IX_FollowingUser_FollowingId",
                table: "FollowingUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Followers",
                table: "Followers");

            migrationBuilder.RenameTable(
                name: "Followers",
                newName: "Follower");

            migrationBuilder.AlterColumn<string>(
                name: "FollowingId",
                table: "FollowingUser",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Follower",
                table: "Follower",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FollowerUser_Follower_FollowerId",
                table: "FollowerUser",
                column: "FollowerId",
                principalTable: "Follower",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

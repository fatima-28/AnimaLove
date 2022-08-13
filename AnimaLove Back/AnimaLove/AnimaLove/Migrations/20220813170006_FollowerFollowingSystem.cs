using Microsoft.EntityFrameworkCore.Migrations;

namespace AnimaLove.Migrations
{
    public partial class FollowerFollowingSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FollowerUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FollowerId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowerUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FollowerUser_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FollowingUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FollowingId = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    AppUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowingUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FollowingUser_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FollowerUser_AppUserId",
                table: "FollowerUser",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_FollowingUser_AppUserId",
                table: "FollowingUser",
                column: "AppUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FollowerUser");

            migrationBuilder.DropTable(
                name: "FollowingUser");
        }
    }
}

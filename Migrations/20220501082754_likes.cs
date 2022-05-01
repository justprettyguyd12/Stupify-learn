using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stupify.Migrations
{
    /// <inheritdoc />
    public partial class likes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLike_Songs_SongId",
                table: "UserLike");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLike_Users_UserId",
                table: "UserLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLike",
                table: "UserLike");

            migrationBuilder.RenameTable(
                name: "UserLike",
                newName: "UserLikes");

            migrationBuilder.RenameIndex(
                name: "IX_UserLike_UserId",
                table: "UserLikes",
                newName: "IX_UserLikes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserLike_SongId",
                table: "UserLikes",
                newName: "IX_UserLikes_SongId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLikes",
                table: "UserLikes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikes_Songs_SongId",
                table: "UserLikes",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikes_Users_UserId",
                table: "UserLikes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLikes_Songs_SongId",
                table: "UserLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserLikes_Users_UserId",
                table: "UserLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserLikes",
                table: "UserLikes");

            migrationBuilder.RenameTable(
                name: "UserLikes",
                newName: "UserLike");

            migrationBuilder.RenameIndex(
                name: "IX_UserLikes_UserId",
                table: "UserLike",
                newName: "IX_UserLike_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserLikes_SongId",
                table: "UserLike",
                newName: "IX_UserLike_SongId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserLike",
                table: "UserLike",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLike_Songs_SongId",
                table: "UserLike",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserLike_Users_UserId",
                table: "UserLike",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

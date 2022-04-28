using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Stupify.Migrations
{
    /// <inheritdoc />
    public partial class songs_and_artists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ArtistId = table.Column<int>(type: "integer", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Songs_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
            
            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "Name", "Description" },
                values: new object[,]
                {
                    { 1, "Неизвестно", "Всякая народная музыка" },
                    { 2, "Slipknot", "Slipknot - nu-metal группа из США. Основана в 1995 году..." },
                    { 3, "Zerodovich", "Zerodovich" }
                });
            
            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "Name", "ArtistId", "Address" },
                values: new object[,]
                {
                    { 1, "Шум леса", 1,
                        "https://noisefx.ru/noise_base/03/01410.mp3" },
                    { 2, "Wait And Bleed", 2,
                        "http://slipknotation.narod.ru/mp3/slipknot/Slipknot_-_Wait_and_bleed.mp3" },
                    { 3, "Баклажан (♂Right Version♂)", 3,
                        "https://api.meowpad.me/v2/sounds/preview/23869.m4a" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Songs_ArtistId",
                table: "Songs",
                column: "ArtistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Songs");

            migrationBuilder.DropTable(
                name: "Artists");
        }
    }
}

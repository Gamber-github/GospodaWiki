using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GospodaWiki.Migrations
{
    /// <inheritdoc />
    public partial class _2049 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlayerId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeriesId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Playes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerSeries",
                columns: table => new
                {
                    PlayerId = table.Column<int>(type: "int", nullable: false),
                    SeriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerSeries", x => new { x.PlayerId, x.SeriesId });
                    table.ForeignKey(
                        name: "FK_PlayerSeries_Playes_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Playes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerSeries_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_PlayerId",
                table: "Tags",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_SeriesId",
                table: "Tags",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerSeries_SeriesId",
                table: "PlayerSeries",
                column: "SeriesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Playes_PlayerId",
                table: "Tags",
                column: "PlayerId",
                principalTable: "Playes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Series_SeriesId",
                table: "Tags",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Playes_PlayerId",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Series_SeriesId",
                table: "Tags");

            migrationBuilder.DropTable(
                name: "PlayerSeries");

            migrationBuilder.DropTable(
                name: "Playes");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropIndex(
                name: "IX_Tags_PlayerId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_SeriesId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "PlayerId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "SeriesId",
                table: "Tags");
        }
    }
}

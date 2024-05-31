using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GospodaWiki.Migrations
{
    /// <inheritdoc />
    public partial class _310520241600 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RpgSystemCharacters");

            migrationBuilder.AddColumn<int>(
                name: "RpgSystemId",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Characters_RpgSystemId",
                table: "Characters",
                column: "RpgSystemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_RpgSystems_RpgSystemId",
                table: "Characters",
                column: "RpgSystemId",
                principalTable: "RpgSystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_RpgSystems_RpgSystemId",
                table: "Characters");

            migrationBuilder.DropIndex(
                name: "IX_Characters_RpgSystemId",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "RpgSystemId",
                table: "Characters");

            migrationBuilder.CreateTable(
                name: "RpgSystemCharacters",
                columns: table => new
                {
                    CharcterId = table.Column<int>(type: "int", nullable: false),
                    RpgSystemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RpgSystemCharacters", x => new { x.CharcterId, x.RpgSystemId });
                    table.ForeignKey(
                        name: "FK_RpgSystemCharacters_Characters_CharcterId",
                        column: x => x.CharcterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RpgSystemCharacters_RpgSystems_RpgSystemId",
                        column: x => x.RpgSystemId,
                        principalTable: "RpgSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RpgSystemCharacters_RpgSystemId",
                table: "RpgSystemCharacters",
                column: "RpgSystemId");
        }
    }
}

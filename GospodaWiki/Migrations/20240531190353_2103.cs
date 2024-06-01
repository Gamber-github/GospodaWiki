using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GospodaWiki.Migrations
{
    /// <inheritdoc />
    public partial class _2103 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerSeries_Playes_PlayerId",
                table: "PlayerSeries");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Playes_PlayerId",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Playes",
                table: "Playes");

            migrationBuilder.RenameTable(
                name: "Playes",
                newName: "Players");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Players",
                table: "Players",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerSeries_Players_PlayerId",
                table: "PlayerSeries",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Players_PlayerId",
                table: "Tags",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayerSeries_Players_PlayerId",
                table: "PlayerSeries");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Players_PlayerId",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Players",
                table: "Players");

            migrationBuilder.RenameTable(
                name: "Players",
                newName: "Playes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Playes",
                table: "Playes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayerSeries_Playes_PlayerId",
                table: "PlayerSeries",
                column: "PlayerId",
                principalTable: "Playes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Playes_PlayerId",
                table: "Tags",
                column: "PlayerId",
                principalTable: "Playes",
                principalColumn: "Id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GospodaWiki.Migrations
{
    /// <inheritdoc />
    public partial class _1551 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e223e93-1f55-4917-ab5e-40085c8a82fa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6584e477-c0f9-4e03-ac39-5aa5fadb4777");

            migrationBuilder.AddColumn<int>(
                name: "GameMasterId",
                table: "Series",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "25b0820d-29f1-4af4-a1eb-47262c119fae", null, "User", "USER" },
                    { "7a93416a-6465-41fa-aa15-85c9a7ce6bf8", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Series_GameMasterId",
                table: "Series",
                column: "GameMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Players_GameMasterId",
                table: "Series",
                column: "GameMasterId",
                principalTable: "Players",
                principalColumn: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Series_Players_GameMasterId",
                table: "Series");

            migrationBuilder.DropIndex(
                name: "IX_Series_GameMasterId",
                table: "Series");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "25b0820d-29f1-4af4-a1eb-47262c119fae");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a93416a-6465-41fa-aa15-85c9a7ce6bf8");

            migrationBuilder.DropColumn(
                name: "GameMasterId",
                table: "Series");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1e223e93-1f55-4917-ab5e-40085c8a82fa", null, "User", "USER" },
                    { "6584e477-c0f9-4e03-ac39-5aa5fadb4777", null, "Admin", "ADMIN" }
                });
        }
    }
}

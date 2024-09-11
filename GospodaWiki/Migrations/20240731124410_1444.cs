using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GospodaWiki.Migrations
{
    /// <inheritdoc />
    public partial class _1444 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stories_RpgSystemId",
                table: "Stories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1bac1269-4e11-4c84-b13e-a61114b07fb9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7006c360-1880-4d74-ab57-31816541a762");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1e223e93-1f55-4917-ab5e-40085c8a82fa", null, "User", "USER" },
                    { "6584e477-c0f9-4e03-ac39-5aa5fadb4777", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stories_RpgSystemId",
                table: "Stories",
                column: "RpgSystemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stories_RpgSystemId",
                table: "Stories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e223e93-1f55-4917-ab5e-40085c8a82fa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6584e477-c0f9-4e03-ac39-5aa5fadb4777");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1bac1269-4e11-4c84-b13e-a61114b07fb9", null, "User", "USER" },
                    { "7006c360-1880-4d74-ab57-31816541a762", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stories_RpgSystemId",
                table: "Stories",
                column: "RpgSystemId",
                unique: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GospodaWiki.Migrations
{
    /// <inheritdoc />
    public partial class _2148 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Equipment");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_EquipmentId",
                table: "Tags",
                column: "EquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Equipment_EquipmentId",
                table: "Tags",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Equipment_EquipmentId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_EquipmentId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "EquipmentId",
                table: "Tags");

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Equipment",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

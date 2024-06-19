using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GospodaWiki.Migrations
{
    /// <inheritdoc />
    public partial class _1241 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isPublished",
                table: "Tags",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isPublished",
                table: "Stories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isPublished",
                table: "Series",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isPublished",
                table: "RpgSystems",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isPublished",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isPublished",
                table: "Locations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isPublished",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isPublished",
                table: "Events",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isPublished",
                table: "Characters",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isPublished",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "isPublished",
                table: "Stories");

            migrationBuilder.DropColumn(
                name: "isPublished",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "isPublished",
                table: "RpgSystems");

            migrationBuilder.DropColumn(
                name: "isPublished",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "isPublished",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "isPublished",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "isPublished",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "isPublished",
                table: "Characters");
        }
    }
}

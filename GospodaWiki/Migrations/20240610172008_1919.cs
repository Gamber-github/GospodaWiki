using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GospodaWiki.Migrations
{
    /// <inheritdoc />
    public partial class _1919 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_RpgSystems_RpgSystemId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Series_SeriesId",
                table: "Characters");

            migrationBuilder.AlterColumn<string>(
                name: "LocationURL",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "SeriesId",
                table: "Characters",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RpgSystemId",
                table: "Characters",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_RpgSystems_RpgSystemId",
                table: "Characters",
                column: "RpgSystemId",
                principalTable: "RpgSystems",
                principalColumn: "RpgSystemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Series_SeriesId",
                table: "Characters",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "SeriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_RpgSystems_RpgSystemId",
                table: "Characters");

            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Series_SeriesId",
                table: "Characters");

            migrationBuilder.AlterColumn<string>(
                name: "LocationURL",
                table: "Locations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SeriesId",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RpgSystemId",
                table: "Characters",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_RpgSystems_RpgSystemId",
                table: "Characters",
                column: "RpgSystemId",
                principalTable: "RpgSystems",
                principalColumn: "RpgSystemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Series_SeriesId",
                table: "Characters",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "SeriesId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

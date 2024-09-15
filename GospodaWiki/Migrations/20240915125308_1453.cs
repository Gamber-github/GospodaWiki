using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GospodaWiki.Migrations
{
    /// <inheritdoc />
    public partial class _1453 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "25b0820d-29f1-4af4-a1eb-47262c119fae");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a93416a-6465-41fa-aa15-85c9a7ce6bf8");

            migrationBuilder.CreateTable(
                name: "Adventures",
                columns: table => new
                {
                    AdventureId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RpgSystemId = table.Column<int>(type: "int", nullable: true),
                    SeriesId = table.Column<int>(type: "int", nullable: true),
                    StoryId = table.Column<int>(type: "int", nullable: true),
                    isPublished = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adventures", x => x.AdventureId);
                    table.ForeignKey(
                        name: "FK_Adventures_RpgSystems_RpgSystemId",
                        column: x => x.RpgSystemId,
                        principalTable: "RpgSystems",
                        principalColumn: "RpgSystemId");
                    table.ForeignKey(
                        name: "FK_Adventures_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "SeriesId");
                    table.ForeignKey(
                        name: "FK_Adventures_Stories_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "StoryId");
                });

            migrationBuilder.CreateTable(
                name: "AdventureTags",
                columns: table => new
                {
                    AdventuresAdventureId = table.Column<int>(type: "int", nullable: false),
                    TagsTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdventureTags", x => new { x.AdventuresAdventureId, x.TagsTagId });
                    table.ForeignKey(
                        name: "FK_AdventureTags_Adventures_AdventuresAdventureId",
                        column: x => x.AdventuresAdventureId,
                        principalTable: "Adventures",
                        principalColumn: "AdventureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdventureTags_Tags_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterAdventures",
                columns: table => new
                {
                    AdventuresAdventureId = table.Column<int>(type: "int", nullable: false),
                    CharactersCharacterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterAdventures", x => new { x.AdventuresAdventureId, x.CharactersCharacterId });
                    table.ForeignKey(
                        name: "FK_CharacterAdventures_Adventures_AdventuresAdventureId",
                        column: x => x.AdventuresAdventureId,
                        principalTable: "Adventures",
                        principalColumn: "AdventureId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterAdventures_Characters_CharactersCharacterId",
                        column: x => x.CharactersCharacterId,
                        principalTable: "Characters",
                        principalColumn: "CharacterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "f0a19d86-9dc9-4f13-a8ca-bb2036cc5d9d", null, "User", "USER" },
                    { "f45be7d0-1d34-4cf4-88da-09197b3b7876", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adventures_RpgSystemId",
                table: "Adventures",
                column: "RpgSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_Adventures_SeriesId",
                table: "Adventures",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Adventures_StoryId",
                table: "Adventures",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AdventureTags_TagsTagId",
                table: "AdventureTags",
                column: "TagsTagId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterAdventures_CharactersCharacterId",
                table: "CharacterAdventures",
                column: "CharactersCharacterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdventureTags");

            migrationBuilder.DropTable(
                name: "CharacterAdventures");

            migrationBuilder.DropTable(
                name: "Adventures");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f0a19d86-9dc9-4f13-a8ca-bb2036cc5d9d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f45be7d0-1d34-4cf4-88da-09197b3b7876");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "25b0820d-29f1-4af4-a1eb-47262c119fae", null, "User", "USER" },
                    { "7a93416a-6465-41fa-aa15-85c9a7ce6bf8", null, "Admin", "ADMIN" }
                });
        }
    }
}

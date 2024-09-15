using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GospodaWiki.Migrations
{
    /// <inheritdoc />
    public partial class _1731 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adventures_Stories_StoryId",
                table: "Adventures");

            migrationBuilder.DropTable(
                name: "StoryTags");

            migrationBuilder.DropTable(
                name: "Stories");

            migrationBuilder.DropIndex(
                name: "IX_Adventures_StoryId",
                table: "Adventures");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f0a19d86-9dc9-4f13-a8ca-bb2036cc5d9d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f45be7d0-1d34-4cf4-88da-09197b3b7876");

            migrationBuilder.DropColumn(
                name: "StoryId",
                table: "Adventures");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "984ea005-c679-44a6-9df7-6f7bb726ab56", null, "Admin", "ADMIN" },
                    { "c9757a4b-2e6d-4db1-896c-fac6bc179a61", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "984ea005-c679-44a6-9df7-6f7bb726ab56");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c9757a4b-2e6d-4db1-896c-fac6bc179a61");

            migrationBuilder.AddColumn<int>(
                name: "StoryId",
                table: "Adventures",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Stories",
                columns: table => new
                {
                    StoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RpgSystemId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YoutubeVideoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isPublished = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stories", x => x.StoryId);
                    table.ForeignKey(
                        name: "FK_Stories_RpgSystems_RpgSystemId",
                        column: x => x.RpgSystemId,
                        principalTable: "RpgSystems",
                        principalColumn: "RpgSystemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoryTags",
                columns: table => new
                {
                    StoriesStoryId = table.Column<int>(type: "int", nullable: false),
                    TagsTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryTags", x => new { x.StoriesStoryId, x.TagsTagId });
                    table.ForeignKey(
                        name: "FK_StoryTags_Stories_StoriesStoryId",
                        column: x => x.StoriesStoryId,
                        principalTable: "Stories",
                        principalColumn: "StoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoryTags_Tags_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
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
                name: "IX_Adventures_StoryId",
                table: "Adventures",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Stories_RpgSystemId",
                table: "Stories",
                column: "RpgSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryTags_TagsTagId",
                table: "StoryTags",
                column: "TagsTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adventures_Stories_StoryId",
                table: "Adventures",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "StoryId");
        }
    }
}

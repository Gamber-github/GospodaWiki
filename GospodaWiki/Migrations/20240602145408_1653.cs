using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GospodaWiki.Migrations
{
    /// <inheritdoc />
    public partial class _1653 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AbilityCharacter_Abilities_AbilitiesAbilityId",
                table: "AbilityCharacter");

            migrationBuilder.DropForeignKey(
                name: "FK_AbilityCharacter_Characters_CharactersCharacterId",
                table: "AbilityCharacter");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterEquipment_Characters_CharactersCharacterId",
                table: "CharacterEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterEquipment_Equipment_EquipmentsEquipmentId",
                table: "CharacterEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterTag_Characters_CharactersCharacterId",
                table: "CharacterTag");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterTag_Tags_TagsTagId",
                table: "CharacterTag");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTag_Equipment_EquipmentId",
                table: "EquipmentTag");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTag_Tags_TagsTagId",
                table: "EquipmentTag");

            migrationBuilder.DropForeignKey(
                name: "FK_EventTag_Events_EventsEventId",
                table: "EventTag");

            migrationBuilder.DropForeignKey(
                name: "FK_EventTag_Tags_TagsTagId",
                table: "EventTag");

            migrationBuilder.DropForeignKey(
                name: "FK_RpgSystemTag_RpgSystems_RpgSystemsRpgSystemId",
                table: "RpgSystemTag");

            migrationBuilder.DropForeignKey(
                name: "FK_RpgSystemTag_Tags_TagsTagId",
                table: "RpgSystemTag");

            migrationBuilder.DropForeignKey(
                name: "FK_SeriesTag_Series_SeriesId",
                table: "SeriesTag");

            migrationBuilder.DropForeignKey(
                name: "FK_SeriesTag_Tags_TagsTagId",
                table: "SeriesTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeriesTag",
                table: "SeriesTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RpgSystemTag",
                table: "RpgSystemTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventTag",
                table: "EventTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentTag",
                table: "EquipmentTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacterTag",
                table: "CharacterTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacterEquipment",
                table: "CharacterEquipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbilityCharacter",
                table: "AbilityCharacter");

            migrationBuilder.RenameTable(
                name: "SeriesTag",
                newName: "SeriesTags");

            migrationBuilder.RenameTable(
                name: "RpgSystemTag",
                newName: "RpgSystemTags");

            migrationBuilder.RenameTable(
                name: "EventTag",
                newName: "EventTags");

            migrationBuilder.RenameTable(
                name: "EquipmentTag",
                newName: "EquipmentTags");

            migrationBuilder.RenameTable(
                name: "CharacterTag",
                newName: "CharacterTags");

            migrationBuilder.RenameTable(
                name: "CharacterEquipment",
                newName: "CharacterEquipments");

            migrationBuilder.RenameTable(
                name: "AbilityCharacter",
                newName: "CharacterAbilities");

            migrationBuilder.RenameIndex(
                name: "IX_SeriesTag_TagsTagId",
                table: "SeriesTags",
                newName: "IX_SeriesTags_TagsTagId");

            migrationBuilder.RenameIndex(
                name: "IX_RpgSystemTag_TagsTagId",
                table: "RpgSystemTags",
                newName: "IX_RpgSystemTags_TagsTagId");

            migrationBuilder.RenameIndex(
                name: "IX_EventTag_TagsTagId",
                table: "EventTags",
                newName: "IX_EventTags_TagsTagId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentTag_TagsTagId",
                table: "EquipmentTags",
                newName: "IX_EquipmentTags_TagsTagId");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterTag_TagsTagId",
                table: "CharacterTags",
                newName: "IX_CharacterTags_TagsTagId");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterEquipment_EquipmentsEquipmentId",
                table: "CharacterEquipments",
                newName: "IX_CharacterEquipments_EquipmentsEquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_AbilityCharacter_CharactersCharacterId",
                table: "CharacterAbilities",
                newName: "IX_CharacterAbilities_CharactersCharacterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeriesTags",
                table: "SeriesTags",
                columns: new[] { "SeriesId", "TagsTagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RpgSystemTags",
                table: "RpgSystemTags",
                columns: new[] { "RpgSystemsRpgSystemId", "TagsTagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventTags",
                table: "EventTags",
                columns: new[] { "EventsEventId", "TagsTagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentTags",
                table: "EquipmentTags",
                columns: new[] { "EquipmentId", "TagsTagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacterTags",
                table: "CharacterTags",
                columns: new[] { "CharactersCharacterId", "TagsTagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacterEquipments",
                table: "CharacterEquipments",
                columns: new[] { "CharactersCharacterId", "EquipmentsEquipmentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacterAbilities",
                table: "CharacterAbilities",
                columns: new[] { "AbilitiesAbilityId", "CharactersCharacterId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterAbilities_Abilities_AbilitiesAbilityId",
                table: "CharacterAbilities",
                column: "AbilitiesAbilityId",
                principalTable: "Abilities",
                principalColumn: "AbilityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterAbilities_Characters_CharactersCharacterId",
                table: "CharacterAbilities",
                column: "CharactersCharacterId",
                principalTable: "Characters",
                principalColumn: "CharacterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterEquipments_Characters_CharactersCharacterId",
                table: "CharacterEquipments",
                column: "CharactersCharacterId",
                principalTable: "Characters",
                principalColumn: "CharacterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterEquipments_Equipment_EquipmentsEquipmentId",
                table: "CharacterEquipments",
                column: "EquipmentsEquipmentId",
                principalTable: "Equipment",
                principalColumn: "EquipmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterTags_Characters_CharactersCharacterId",
                table: "CharacterTags",
                column: "CharactersCharacterId",
                principalTable: "Characters",
                principalColumn: "CharacterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterTags_Tags_TagsTagId",
                table: "CharacterTags",
                column: "TagsTagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTags_Equipment_EquipmentId",
                table: "EquipmentTags",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "EquipmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTags_Tags_TagsTagId",
                table: "EquipmentTags",
                column: "TagsTagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventTags_Events_EventsEventId",
                table: "EventTags",
                column: "EventsEventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventTags_Tags_TagsTagId",
                table: "EventTags",
                column: "TagsTagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RpgSystemTags_RpgSystems_RpgSystemsRpgSystemId",
                table: "RpgSystemTags",
                column: "RpgSystemsRpgSystemId",
                principalTable: "RpgSystems",
                principalColumn: "RpgSystemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RpgSystemTags_Tags_TagsTagId",
                table: "RpgSystemTags",
                column: "TagsTagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeriesTags_Series_SeriesId",
                table: "SeriesTags",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "SeriesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeriesTags_Tags_TagsTagId",
                table: "SeriesTags",
                column: "TagsTagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CharacterAbilities_Abilities_AbilitiesAbilityId",
                table: "CharacterAbilities");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterAbilities_Characters_CharactersCharacterId",
                table: "CharacterAbilities");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterEquipments_Characters_CharactersCharacterId",
                table: "CharacterEquipments");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterEquipments_Equipment_EquipmentsEquipmentId",
                table: "CharacterEquipments");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterTags_Characters_CharactersCharacterId",
                table: "CharacterTags");

            migrationBuilder.DropForeignKey(
                name: "FK_CharacterTags_Tags_TagsTagId",
                table: "CharacterTags");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTags_Equipment_EquipmentId",
                table: "EquipmentTags");

            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentTags_Tags_TagsTagId",
                table: "EquipmentTags");

            migrationBuilder.DropForeignKey(
                name: "FK_EventTags_Events_EventsEventId",
                table: "EventTags");

            migrationBuilder.DropForeignKey(
                name: "FK_EventTags_Tags_TagsTagId",
                table: "EventTags");

            migrationBuilder.DropForeignKey(
                name: "FK_RpgSystemTags_RpgSystems_RpgSystemsRpgSystemId",
                table: "RpgSystemTags");

            migrationBuilder.DropForeignKey(
                name: "FK_RpgSystemTags_Tags_TagsTagId",
                table: "RpgSystemTags");

            migrationBuilder.DropForeignKey(
                name: "FK_SeriesTags_Series_SeriesId",
                table: "SeriesTags");

            migrationBuilder.DropForeignKey(
                name: "FK_SeriesTags_Tags_TagsTagId",
                table: "SeriesTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SeriesTags",
                table: "SeriesTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RpgSystemTags",
                table: "RpgSystemTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventTags",
                table: "EventTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EquipmentTags",
                table: "EquipmentTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacterTags",
                table: "CharacterTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacterEquipments",
                table: "CharacterEquipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CharacterAbilities",
                table: "CharacterAbilities");

            migrationBuilder.RenameTable(
                name: "SeriesTags",
                newName: "SeriesTag");

            migrationBuilder.RenameTable(
                name: "RpgSystemTags",
                newName: "RpgSystemTag");

            migrationBuilder.RenameTable(
                name: "EventTags",
                newName: "EventTag");

            migrationBuilder.RenameTable(
                name: "EquipmentTags",
                newName: "EquipmentTag");

            migrationBuilder.RenameTable(
                name: "CharacterTags",
                newName: "CharacterTag");

            migrationBuilder.RenameTable(
                name: "CharacterEquipments",
                newName: "CharacterEquipment");

            migrationBuilder.RenameTable(
                name: "CharacterAbilities",
                newName: "AbilityCharacter");

            migrationBuilder.RenameIndex(
                name: "IX_SeriesTags_TagsTagId",
                table: "SeriesTag",
                newName: "IX_SeriesTag_TagsTagId");

            migrationBuilder.RenameIndex(
                name: "IX_RpgSystemTags_TagsTagId",
                table: "RpgSystemTag",
                newName: "IX_RpgSystemTag_TagsTagId");

            migrationBuilder.RenameIndex(
                name: "IX_EventTags_TagsTagId",
                table: "EventTag",
                newName: "IX_EventTag_TagsTagId");

            migrationBuilder.RenameIndex(
                name: "IX_EquipmentTags_TagsTagId",
                table: "EquipmentTag",
                newName: "IX_EquipmentTag_TagsTagId");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterTags_TagsTagId",
                table: "CharacterTag",
                newName: "IX_CharacterTag_TagsTagId");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterEquipments_EquipmentsEquipmentId",
                table: "CharacterEquipment",
                newName: "IX_CharacterEquipment_EquipmentsEquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_CharacterAbilities_CharactersCharacterId",
                table: "AbilityCharacter",
                newName: "IX_AbilityCharacter_CharactersCharacterId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SeriesTag",
                table: "SeriesTag",
                columns: new[] { "SeriesId", "TagsTagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RpgSystemTag",
                table: "RpgSystemTag",
                columns: new[] { "RpgSystemsRpgSystemId", "TagsTagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventTag",
                table: "EventTag",
                columns: new[] { "EventsEventId", "TagsTagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_EquipmentTag",
                table: "EquipmentTag",
                columns: new[] { "EquipmentId", "TagsTagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacterTag",
                table: "CharacterTag",
                columns: new[] { "CharactersCharacterId", "TagsTagId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CharacterEquipment",
                table: "CharacterEquipment",
                columns: new[] { "CharactersCharacterId", "EquipmentsEquipmentId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbilityCharacter",
                table: "AbilityCharacter",
                columns: new[] { "AbilitiesAbilityId", "CharactersCharacterId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AbilityCharacter_Abilities_AbilitiesAbilityId",
                table: "AbilityCharacter",
                column: "AbilitiesAbilityId",
                principalTable: "Abilities",
                principalColumn: "AbilityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AbilityCharacter_Characters_CharactersCharacterId",
                table: "AbilityCharacter",
                column: "CharactersCharacterId",
                principalTable: "Characters",
                principalColumn: "CharacterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterEquipment_Characters_CharactersCharacterId",
                table: "CharacterEquipment",
                column: "CharactersCharacterId",
                principalTable: "Characters",
                principalColumn: "CharacterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterEquipment_Equipment_EquipmentsEquipmentId",
                table: "CharacterEquipment",
                column: "EquipmentsEquipmentId",
                principalTable: "Equipment",
                principalColumn: "EquipmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterTag_Characters_CharactersCharacterId",
                table: "CharacterTag",
                column: "CharactersCharacterId",
                principalTable: "Characters",
                principalColumn: "CharacterId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CharacterTag_Tags_TagsTagId",
                table: "CharacterTag",
                column: "TagsTagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTag_Equipment_EquipmentId",
                table: "EquipmentTag",
                column: "EquipmentId",
                principalTable: "Equipment",
                principalColumn: "EquipmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentTag_Tags_TagsTagId",
                table: "EquipmentTag",
                column: "TagsTagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventTag_Events_EventsEventId",
                table: "EventTag",
                column: "EventsEventId",
                principalTable: "Events",
                principalColumn: "EventId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventTag_Tags_TagsTagId",
                table: "EventTag",
                column: "TagsTagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RpgSystemTag_RpgSystems_RpgSystemsRpgSystemId",
                table: "RpgSystemTag",
                column: "RpgSystemsRpgSystemId",
                principalTable: "RpgSystems",
                principalColumn: "RpgSystemId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RpgSystemTag_Tags_TagsTagId",
                table: "RpgSystemTag",
                column: "TagsTagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeriesTag_Series_SeriesId",
                table: "SeriesTag",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "SeriesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeriesTag_Tags_TagsTagId",
                table: "SeriesTag",
                column: "TagsTagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

﻿// <auto-generated />
using System;
using GospodaWiki.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GospodaWiki.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GospodaWiki.Models.Ability", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Abilities");
                });

            modelBuilder.Entity("GospodaWiki.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Biography")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RpgSystemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("RpgSystemId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("GospodaWiki.Models.CharacterAbility", b =>
                {
                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("AbilityId")
                        .HasColumnType("int");

                    b.HasKey("CharacterId", "AbilityId");

                    b.HasIndex("AbilityId");

                    b.ToTable("CharacterAbilities");
                });

            modelBuilder.Entity("GospodaWiki.Models.CharacterEquipment", b =>
                {
                    b.Property<int>("EquipmentId")
                        .HasColumnType("int");

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.HasKey("EquipmentId", "CharacterId");

                    b.HasIndex("CharacterId");

                    b.ToTable("CharacterEquipments");
                });

            modelBuilder.Entity("GospodaWiki.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("GospodaWiki.Models.Equipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Equipment");
                });

            modelBuilder.Entity("GospodaWiki.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("GospodaWiki.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("GospodaWiki.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("About")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("GospodaWiki.Models.PlayerSeries", b =>
                {
                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("SeriesId")
                        .HasColumnType("int");

                    b.HasKey("PlayerId", "SeriesId");

                    b.HasIndex("SeriesId");

                    b.ToTable("PlayerSeries");
                });

            modelBuilder.Entity("GospodaWiki.Models.RpgSystem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("RpgSystems");
                });

            modelBuilder.Entity("GospodaWiki.Models.Series", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Series");
                });

            modelBuilder.Entity("GospodaWiki.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int?>("EquipmentId")
                        .HasColumnType("int");

                    b.Property<int?>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int?>("RpgSystemId")
                        .HasColumnType("int");

                    b.Property<int?>("SeriesId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId");

                    b.HasIndex("EquipmentId");

                    b.HasIndex("EventId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("RpgSystemId");

                    b.HasIndex("SeriesId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("GospodaWiki.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GospodaWiki.Models.Character", b =>
                {
                    b.HasOne("GospodaWiki.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GospodaWiki.Models.RpgSystem", "RpgSystem")
                        .WithMany()
                        .HasForeignKey("RpgSystemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("RpgSystem");
                });

            modelBuilder.Entity("GospodaWiki.Models.CharacterAbility", b =>
                {
                    b.HasOne("GospodaWiki.Models.Ability", "Ability")
                        .WithMany("CharacterAbilities")
                        .HasForeignKey("AbilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GospodaWiki.Models.Character", "Character")
                        .WithMany("CharacterAbilities")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ability");

                    b.Navigation("Character");
                });

            modelBuilder.Entity("GospodaWiki.Models.CharacterEquipment", b =>
                {
                    b.HasOne("GospodaWiki.Models.Character", "Character")
                        .WithMany("CharacterEquipments")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GospodaWiki.Models.Equipment", "Equipment")
                        .WithMany("CharacterEquipments")
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("Equipment");
                });

            modelBuilder.Entity("GospodaWiki.Models.Event", b =>
                {
                    b.HasOne("GospodaWiki.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");
                });

            modelBuilder.Entity("GospodaWiki.Models.PlayerSeries", b =>
                {
                    b.HasOne("GospodaWiki.Models.Player", "Player")
                        .WithMany("PlayerSeries")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GospodaWiki.Models.Series", "Series")
                        .WithMany("PlayerSeries")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");

                    b.Navigation("Series");
                });

            modelBuilder.Entity("GospodaWiki.Models.Tag", b =>
                {
                    b.HasOne("GospodaWiki.Models.Character", null)
                        .WithMany("Tags")
                        .HasForeignKey("CharacterId");

                    b.HasOne("GospodaWiki.Models.Equipment", null)
                        .WithMany("Tags")
                        .HasForeignKey("EquipmentId");

                    b.HasOne("GospodaWiki.Models.Event", null)
                        .WithMany("Tags")
                        .HasForeignKey("EventId");

                    b.HasOne("GospodaWiki.Models.Player", null)
                        .WithMany("Tags")
                        .HasForeignKey("PlayerId");

                    b.HasOne("GospodaWiki.Models.RpgSystem", null)
                        .WithMany("Tags")
                        .HasForeignKey("RpgSystemId");

                    b.HasOne("GospodaWiki.Models.Series", null)
                        .WithMany("Tags")
                        .HasForeignKey("SeriesId");
                });

            modelBuilder.Entity("GospodaWiki.Models.Ability", b =>
                {
                    b.Navigation("CharacterAbilities");
                });

            modelBuilder.Entity("GospodaWiki.Models.Character", b =>
                {
                    b.Navigation("CharacterAbilities");

                    b.Navigation("CharacterEquipments");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("GospodaWiki.Models.Equipment", b =>
                {
                    b.Navigation("CharacterEquipments");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("GospodaWiki.Models.Event", b =>
                {
                    b.Navigation("Tags");
                });

            modelBuilder.Entity("GospodaWiki.Models.Player", b =>
                {
                    b.Navigation("PlayerSeries");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("GospodaWiki.Models.RpgSystem", b =>
                {
                    b.Navigation("Tags");
                });

            modelBuilder.Entity("GospodaWiki.Models.Series", b =>
                {
                    b.Navigation("PlayerSeries");

                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}

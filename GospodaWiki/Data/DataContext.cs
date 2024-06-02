using GospodaWiki.Models;
using Microsoft.EntityFrameworkCore;

namespace GospodaWiki.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Ability> Abilities { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<RpgSystem> RpgSystems { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Player> Players { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>()
                .HasMany(c => c.Tags)
                .WithMany(t => t.Characters)
                .UsingEntity(j => j.ToTable("CharacterTags"));

            modelBuilder.Entity<RpgSystem>()
                .HasMany(r => r.Tags)
                .WithMany(t => t.RpgSystems)
                .UsingEntity(j => j.ToTable("RpgSystemTags"));

            modelBuilder.Entity<RpgSystem>()
                .HasMany(r => r.Characters)
                .WithOne(c => c.RpgSystem);

            modelBuilder.Entity<RpgSystem>()
                .HasMany(r => r.Series)
                .WithOne(s => s.RpgSystem);

            modelBuilder.Entity<Series>()
                .HasMany(s => s.Tags)
                .WithMany(t => t.Series)
                .UsingEntity(j => j.ToTable("SeriesTags"));

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Tags)
                .WithMany(t => t.Events)
                .UsingEntity(j => j.ToTable("EventTags"));

            modelBuilder.Entity<Equipment>()
                .HasMany(e => e.Tags)
                .WithMany(t => t.Equipment)
                .UsingEntity(j => j.ToTable("EquipmentTags"));

            modelBuilder.Entity<Ability>()
                .HasMany(c => c.Characters)
                .WithMany(a => a.Abilities)
                .UsingEntity(j => j.ToTable("CharacterAbilities"));

            modelBuilder.Entity<Equipment>()
                .HasMany(c => c.Characters)
                .WithMany(e => e.Equipments)
                .UsingEntity(j => j.ToTable("CharacterEquipments"));

            modelBuilder.Entity<Country>()
                .HasMany(c => c.Characters)
                .WithOne(c => c.Country);

            modelBuilder.Entity<Location>()
                .HasMany(l => l.Events)
                .WithOne(l => l.Location);

            modelBuilder.Entity<Player>()
                .HasMany(p => p.Series)
                .WithMany(s => s.Players)
                .UsingEntity(j => j.ToTable("PlayerSeries"));
        }
    }
}
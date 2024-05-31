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
        public DbSet<CharacterAbility> CharacterAbilities { get; set; }
        public DbSet<CharacterEquipment> CharacterEquipments { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<RpgSystem> RpgSystems { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; } 
        public DbSet<Location> Locations { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerSeries> PlayerSeries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CharacterAbility>()
                .HasKey(ca => new { ca.CharacterId, ca.AbilityId });
            modelBuilder.Entity<CharacterAbility>()
                .HasOne(c => c.Character)
                .WithMany(ca => ca.CharacterAbilities)
                .HasForeignKey(c => c.CharacterId);
            modelBuilder.Entity<CharacterAbility>()
                .HasOne(a => a.Ability)
                .WithMany(ca => ca.CharacterAbilities)
                .HasForeignKey(a => a.AbilityId);

            modelBuilder.Entity<CharacterEquipment>()
                .HasKey(ce => new { ce.EquipmentId, ce.CharacterId });
            modelBuilder.Entity<CharacterEquipment>()
                .HasOne(c => c.Character)
                .WithMany(ce => ce.CharacterEquipments)
                .HasForeignKey(c => c.CharacterId);
            modelBuilder.Entity<CharacterEquipment>()
                .HasOne(e => e.Equipment)
                .WithMany(ce => ce.CharacterEquipments)
                .HasForeignKey(a => a.EquipmentId);

            modelBuilder.Entity<PlayerSeries>()
                .HasKey(ps => new { ps.PlayerId, ps.SeriesId });
            modelBuilder.Entity<PlayerSeries>()
                .HasOne(p => p.Player)
                .WithMany(ps => ps.PlayerSeries)
                .HasForeignKey(p => p.PlayerId);
            modelBuilder.Entity<PlayerSeries>()
                .HasOne(s => s.Series)
                .WithMany(ps => ps.PlayerSeries)
                .HasForeignKey(s => s.SeriesId);
        }
    }
}
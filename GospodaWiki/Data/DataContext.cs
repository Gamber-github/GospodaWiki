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
        public DbSet<RpgSystemCharacter> RpgSystemCharacters { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }

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

            modelBuilder.Entity<RpgSystemCharacter>()
                .HasKey(sc => new { sc.CharcterId, sc.RpgSystemId });
            modelBuilder.Entity<RpgSystemCharacter>()
                .HasOne(s => s.RpgSystem)
                .WithMany(sc => sc.RpgSystemCharacters)
                .HasForeignKey(s => s.RpgSystemId);
            modelBuilder.Entity<RpgSystemCharacter>()
                .HasOne(c => c.Character)
                .WithMany(ce => ce.RpgSystemCharacters)
                .HasForeignKey(c => c.CharcterId);
        }
    }
}
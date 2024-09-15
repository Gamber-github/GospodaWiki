using GospodaWiki.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GospodaWiki.Data
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Character> Characters { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<RpgSystem> RpgSystems { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Adventure> Adventures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            List<IdentityRole> roles = new List<IdentityRole>
                {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);

            modelBuilder.Entity<Character>()
                .HasMany(c => c.Tags)
                .WithMany(t => t.Characters)
                .UsingEntity(j => j.ToTable("CharacterTags"));

            modelBuilder.Entity<Character>()
                .HasMany(c => c.Items)
                .WithMany(i => i.Characters)
                .UsingEntity(j => j.ToTable("CharacterItems"));

            modelBuilder.Entity<Character>()
                .HasMany(c => c.Adventures)
                .WithMany(a => a.Characters)
                .UsingEntity(j => j.ToTable("CharacterAdventures"));

            modelBuilder.Entity<Item>()
                .HasMany(c => c.Characters)
                .WithMany(e => e.Items)
                .UsingEntity(j => j.ToTable("CharacterItems"));

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

            modelBuilder.Entity<RpgSystem>()
                .HasMany(a => a.Adventures)
                .WithOne(a => a.RpgSystem);

            modelBuilder.Entity<Series>()
                .HasMany(s => s.Tags)
                .WithMany(t => t.Series)
                .UsingEntity(j => j.ToTable("SeriesTags"));

            modelBuilder.Entity<Series>()
                .HasMany(p => p.Players)
                .WithMany(p => p.Series)
                .UsingEntity(j => j.ToTable("PlayerSeries"));

            modelBuilder.Entity<Series>()
                .HasMany(c => c.Characters)
                .WithOne(c => c.Series);

            modelBuilder.Entity<Series>()
                .HasMany(s => s.Adventures)
                .WithOne(a => a.Series);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Tags)
                .WithMany(t => t.Events)
                .UsingEntity(j => j.ToTable("EventTags"));

            modelBuilder.Entity<Item>()
                .HasMany(e => e.Tags)
                .WithMany(t => t.Items)
                .UsingEntity(j => j.ToTable("ItemsTags"));

            modelBuilder.Entity<Location>()
                .HasMany(l => l.Events)
                .WithOne(l => l.Location);

            modelBuilder.Entity<Series>()
                .HasOne(s => s.GameMaster)
                .WithMany()
                .HasForeignKey(s => s.GameMasterId);

            modelBuilder.Entity<Adventure>()
                .HasMany(a => a.Tags)
                .WithMany(t => t.Adventures)
                .UsingEntity(j => j.ToTable("AdventureTags"));

            modelBuilder.Entity<Adventure>()
                 .HasOne(r => r.RpgSystem)
                 .WithMany(a => a.Adventures);


            modelBuilder.Entity<Adventure>()
                .HasOne(a => a.Series)
                .WithMany(a => a.Adventures);

            modelBuilder.Entity<Adventure>()
                .HasMany(a => a.Characters)
                .WithMany(c => c.Adventures)
                .UsingEntity(j => j.ToTable("CharacterAdventures"));
        }
    }
}
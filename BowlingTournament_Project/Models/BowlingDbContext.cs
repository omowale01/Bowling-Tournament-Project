using BowlingTournament_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace BowlingTournament_Project.Models
{
    public class BowlingDbContext : DbContext
    {
        public BowlingDbContext(DbContextOptions<BowlingDbContext> options)
            : base(options)
        {
        }

        public DbSet<BowlingUser> BowlingUsers => Set<BowlingUser>();
        public DbSet<Division> Divisions => Set<Division>();
        public DbSet<Team> Teams => Set<Team>();
        public DbSet<Player> Players => Set<Player>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BowlingUser>().ToTable("BowlingUser");
            modelBuilder.Entity<Division>().ToTable("Division");
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<Player>().ToTable("Player");
        }
    }
}

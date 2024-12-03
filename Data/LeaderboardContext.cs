using Leaderboard.Models;
using Microsoft.EntityFrameworkCore;

namespace Leaderboard.Data
{
    public class LeaderboardContext : DbContext
    {
        public LeaderboardContext(DbContextOptions<LeaderboardContext> options) : base(options) { }

        public DbSet<Score> Scores { get; set; } // Table for scores

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Score>()
                .HasKey(s => new { s.PlayerId, s.Timestamp }); // Composite key
        }
    }


}

using Leaderboard.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Leaderboard.Data
{
    public class LeaderboardContext : IdentityDbContext<ApplicationUser>
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

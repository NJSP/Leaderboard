namespace Leaderboard.Models
{
    public class Score
    {
        public Guid PlayerId { get; set; } // Unique identifier for each player
        public string? playerName { get; set; } // The player's name
        public int Value { get; set; } // The player's score
        public DateTime Timestamp { get; set; } // When the score was set
    }
}

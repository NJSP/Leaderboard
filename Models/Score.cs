using Leaderboard.Models;

public class Score
{
    public Guid PlayerId { get; set; }
    public string? playerName { get; set; }
    public int Value { get; set; }
    public DateTime Timestamp { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}
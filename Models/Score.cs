using Leaderboard.Models;

public class Score
{
    public Guid PlayerId { get; set; }
    public string? playerName { get; set; }
    public int Value { get; set; }
    public DateTime Timestamp { get; set; }
    public required string UserId { get; set; }
    public required ApplicationUser User { get; set; }
}
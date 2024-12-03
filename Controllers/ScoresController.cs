using Leaderboard;
using Leaderboard.Data;
using Leaderboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace LeaderboardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScoresController : ControllerBase
    {
        private readonly LeaderboardContext _context;
        private readonly IHubContext<ScoresHub> _hubContext;

        public ScoresController(LeaderboardContext context, IHubContext<ScoresHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        // POST /api/scores
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateScore([FromBody] Score score)
        {
            var existingScore = await _context.Scores.FindAsync(score.PlayerId, score.Timestamp);

            if (existingScore != null)
            {
                // Update existing score if higher
                if (score.Value > existingScore.Value)
                {
                    existingScore.Value = score.Value;
                    existingScore.Timestamp = DateTime.UtcNow;
                }
            }
            else
            {
                // Add new score
                score.Timestamp = DateTime.UtcNow;
                await _context.Scores.AddAsync(score);
            }

            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReceiveScoreUpdate", score); // Send real-time update
            return Ok(score);
        }

        // Other methods remain unchanged
    }
}

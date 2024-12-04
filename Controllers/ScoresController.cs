using Leaderboard;
using Leaderboard.Data;
using Leaderboard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

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

        // GET /api/scores/top?n=10
        [HttpGet("top")]
        public async Task<ActionResult<IEnumerable<Score>>> GetTopScores([FromQuery] int n = 10)
        {
            var topScores = await _context.Scores
                .OrderByDescending(s => s.Value)
                .Take(n)
                .ToListAsync();

            return Ok(topScores);
        }

        // GET /api/scores/{playerName}
        [HttpGet("{playerName}")]
        public async Task<ActionResult<IEnumerable<Score>>> GetScores(string playerName)
        {
            var scores = await _context.Scores
                .Where(s => s.playerName == playerName)
                .ToListAsync();

            return Ok(scores);
        }

        // DELETE /api/scores/{playerId}/{timestamp}
        [HttpDelete("{playerId}/{timestamp}")]
        public async Task<IActionResult> DeleteScore(Guid playerId, DateTime timestamp)
        {
            var score = await _context.Scores
                .FirstOrDefaultAsync(s => s.PlayerId == playerId && s.Timestamp == timestamp);

            if (score == null)
            {
                return NotFound();
            }

            _context.Scores.Remove(score);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE /api/scores
        [HttpDelete]
        public async Task<IActionResult> DeleteAllScores()
        {
            _context.Scores.RemoveRange(_context.Scores);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
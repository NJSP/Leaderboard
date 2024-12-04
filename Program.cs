using Leaderboard.Data;
using Microsoft.EntityFrameworkCore;
using Leaderboard;

namespace Leaderboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDbContext<LeaderboardContext>(options =>
                options.UseSqlite("Data Source=leaderboard.db")); // Use SQLite
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSignalR(); // Add SignalR


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<LeaderboardContext>();
                db.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseStaticFiles(); // Enable serving static files
            app.UseDefaultFiles(); // Enable default file mapping (e.g., index.html)
            app.MapControllers();
            app.MapHub<ScoresHub>("/scoresHub"); // Map SignalR hub

            app.Run();
        }
    }
}
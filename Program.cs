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

            builder.Services.AddControllers();
            builder.Services.AddDbContext<LeaderboardContext>(options =>
                options.UseSqlite("Data Source=leaderboard.db"));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSignalR();


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<LeaderboardContext>();
                db.Database.Migrate();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.MapControllers();
            app.MapHub<ScoresHub>("/scoresHub");

            app.Run();
        }
    }
}
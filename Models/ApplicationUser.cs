using Microsoft.AspNetCore.Identity;

namespace Leaderboard.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string PlayerName { get; set; }
    }

    public class RegisterModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string PlayerName { get; set; }
    }

    public class LoginModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
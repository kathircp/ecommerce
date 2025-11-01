using System.ComponentModel.DataAnnotations;

namespace ECommerce.DTOs
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }

    public class LoginResponse
    {
        public string Token { get; set; } = null!;
        public string? TokenType { get; set; } = "Bearer";
    }
}
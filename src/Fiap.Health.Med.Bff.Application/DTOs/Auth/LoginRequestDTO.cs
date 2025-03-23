using System.ComponentModel.DataAnnotations;

namespace Fiap.Health.Med.Bff.Application.DTOs.Auth
{
    public class LoginRequestDTO
    {
        [Length(2, 2, ErrorMessage = "UF must have 2 characters")]
        public string? Uf { get; set; }

        [Required(ErrorMessage = "Username is required")]
        public required string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; } = string.Empty;
    }
}

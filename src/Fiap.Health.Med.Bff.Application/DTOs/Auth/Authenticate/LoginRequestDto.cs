using System.ComponentModel.DataAnnotations;

namespace Fiap.Health.Med.Bff.Application.DTOs.Auth.Authenticate
{
    public class LoginRequestDto
    {
        [Length(2, 2, ErrorMessage = "Concil state must have 2 characters")]
        public string? Uf { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Username is required")]
        public required int Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; } = string.Empty;
    }
}

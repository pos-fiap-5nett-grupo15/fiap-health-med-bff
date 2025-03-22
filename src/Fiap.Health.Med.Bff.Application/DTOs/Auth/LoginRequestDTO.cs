namespace Fiap.Health.Med.Bff.Application.DTOs.Auth
{
    public class LoginRequestDTO
    {
        public string? Uf { get; set; }
        public required string Username { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
    }
}

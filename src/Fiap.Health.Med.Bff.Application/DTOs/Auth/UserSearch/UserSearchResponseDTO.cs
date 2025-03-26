using Fiap.Health.Med.Infra.Enums;

namespace Fiap.Health.Med.Bff.Application.DTOs.Auth.UserSearch
{
    public class UserSearchResponseDTO
    {
        public required string Username { get; set; }
        public required string HashPassword { get; set; }
        public EUserType UserType { get; set; }
    }
}

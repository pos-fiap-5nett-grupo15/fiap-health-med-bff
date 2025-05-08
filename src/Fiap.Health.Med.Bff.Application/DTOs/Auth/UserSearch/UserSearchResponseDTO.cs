using Fiap.Health.Med.Bff.Domain.Enums;

namespace Fiap.Health.Med.Bff.Application.Dtos.Auth.UserSearch
{
    public class UserSearchResponseDto
    {
        public required string Username { get; set; }
        public required string HashPassword { get; set; }
        public EUserType UserType { get; set; }
    }
}

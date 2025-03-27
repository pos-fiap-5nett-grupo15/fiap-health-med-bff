using Fiap.Health.Med.Bff.Application.DTOs.Auth.UserSearch;

namespace Fiap.Health.Med.Bff.Application.Interfaces.Auth
{
    public interface ITokenHandler
    {
        string GenerateToken(UserSearchResponseDTO userData);
    }
}

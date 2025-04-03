using Fiap.Health.Med.Bff.Application.Dtos.Auth.UserSearch;

namespace Fiap.Health.Med.Bff.Application.Interfaces.Auth
{
    public interface ITokenHandler
    {
        string GenerateToken(UserSearchResponseDto userData);
    }
}

using Fiap.Health.Med.Infra.DTOs.Auth;

namespace Fiap.Health.Med.Bff.Application.Interfaces.Auth
{
    public interface ITokenHandler
    {
        string GenerateToken(SearchUserResponseDTO userData);
    }
}

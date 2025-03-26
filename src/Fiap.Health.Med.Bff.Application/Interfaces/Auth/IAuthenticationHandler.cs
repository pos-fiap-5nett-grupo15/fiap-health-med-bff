using Fiap.Health.Med.Bff.Application.DTOs.Auth;

namespace Fiap.Health.Med.Bff.Application.Interfaces.Auth
{
    public interface IAuthenticationHandler
    {
        Task<LoginResponseDTO> AuthenticateAsync(LoginRequestDTO requestData);
    }
}

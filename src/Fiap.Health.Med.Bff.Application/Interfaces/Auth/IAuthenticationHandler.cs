using Fiap.Health.Med.Bff.Application.DTOs.Auth.Authenticate;

namespace Fiap.Health.Med.Bff.Application.Interfaces.Auth
{
    public interface IAuthenticationHandler
    {
        Task<LoginResponseDto> AuthenticateAsync(LoginRequestDto requestData);
    }
}

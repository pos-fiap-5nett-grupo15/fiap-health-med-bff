using Fiap.Health.Med.Bff.Application.Dtos.Auth.UserSearch;
using Fiap.Health.Med.Bff.Application.Interfaces.Auth;
using Fiap.Health.Med.Bff.CrossCutting.Settings;
using Fiap.Health.Med.Bff.Infrastructure.Http.Interfaces;
using Microsoft.Extensions.Options;
using BCrypt;
using RestSharp;
using Fiap.Health.Med.Bff.Application.DTOs.Auth.Authenticate;
using Fiap.Health.Med.Bff.Application.Interfaces.Doctor;
using Fiap.Health.Med.Bff.Application.Interfaces.Patient;

namespace Fiap.Health.Med.Bff.Application.Handlers
{
    public class AuthenticationHandler : IAuthenticationHandler
    {
        private readonly IApiClient _apiClient;
        private readonly ITokenHandler _tokenHandler;
        private readonly IDoctorHandler _doctorHandler;
        private readonly IPatientHandler _patientHandler;
        private readonly ExternalServicesSettings _extenalApiSettings;

        public AuthenticationHandler(IApiClient apiClient, ITokenHandler tokenHandler, IDoctorHandler doctorHandler, IPatientHandler patientHandler, IOptions<ExternalServicesSettings> extenalApiSettings)
        {
            _apiClient = apiClient;
            _tokenHandler = tokenHandler;
            _doctorHandler = doctorHandler;
            _patientHandler = patientHandler;
            _extenalApiSettings = extenalApiSettings.Value;
        }

        public async Task<LoginResponseDto> AuthenticateAsync(LoginRequestDto requestData)
        {
            UserSearchResponseDto? userSearchResponseDto;

            if (!string.IsNullOrEmpty(requestData.Uf))
            {
                userSearchResponseDto = await _doctorHandler.GetDoctorByConcilAsync(requestData.Uf, requestData.Username);
            }
            else
            {
                userSearchResponseDto = await _patientHandler.GetPatientByCpfAsync(requestData.Username);
            }

            if (userSearchResponseDto is not null)
            {
                if (BCryptHelper.CheckPassword(requestData.Password, userSearchResponseDto.HashPassword))
                {
                    var token = _tokenHandler.GenerateToken(userSearchResponseDto);
                    return new LoginResponseDto()
                    {
                        AccessToken = token,
                        Message = string.IsNullOrEmpty(token) ? "Unable to generate token" : string.Empty
                    };
                }
                else
                    return new LoginResponseDto()
                    {
                        AccessToken = string.Empty,
                        Message = "Invalid password"
                    };
            }
            else
            {
                return new LoginResponseDto()
                {
                    AccessToken = string.Empty,
                    Message = "User not found"
                };
            }
        }
    }
}

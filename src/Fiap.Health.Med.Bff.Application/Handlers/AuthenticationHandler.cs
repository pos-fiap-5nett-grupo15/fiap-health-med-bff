using Fiap.Health.Med.Bff.Application.DTOs.Auth;
using Fiap.Health.Med.Bff.Application.DTOs.Auth.UserSearch;
using Fiap.Health.Med.Bff.Application.Interfaces.Auth;
using Fiap.Health.Med.Bff.CrossCutting.Settings;
using Fiap.Health.Med.Bff.Infrastructure.Http.Interfaces;
using Fiap.Health.Med.Infra.Enums;
using Microsoft.Extensions.Options;

namespace Fiap.Health.Med.Bff.Application.Handlers
{
    public class AuthenticationHandler : IAuthenticationHandler
    {
        private readonly IApiClient _apiClient;
        private readonly ITokenHandler _tokenHandler;
        private readonly ExternalServicesSettings _extenalApiSettings;

        public AuthenticationHandler(IApiClient apiClient, ITokenHandler tokenHandler, IOptions<ExternalServicesSettings> extenalApiSettings)
        {
            _apiClient = apiClient;
            _tokenHandler = tokenHandler;
            _extenalApiSettings = extenalApiSettings.Value;
        }

        public async Task<LoginResponseDTO> AuthenticateAsync(LoginRequestDTO requestData)
        {
            object searcUserRequest;

            if (!string.IsNullOrEmpty(requestData.Uf))
                searcUserRequest = new DoctorSearchByConcilDTO() { ConcilNumer = requestData.Username, Uf = requestData.Uf };
            else
                searcUserRequest = new PatientSearchByDocumentDTO() { Document = requestData.Username };

#if DEBUG
            var tempResp = new UserSearchResponseDTO()
            {
                Username = requestData.Username,
                HashPassword = BCrypt.BCryptHelper.HashPassword("teste123", BCrypt.BCryptHelper.GenerateSalt()),
                UserType = string.IsNullOrEmpty(requestData.Uf) ? EUserType.Patient : EUserType.Doctor
            };

            if (BCrypt.BCryptHelper.CheckPassword(requestData.Password, tempResp.HashPassword))
            {
                var token = _tokenHandler.GenerateToken(tempResp);
                return new LoginResponseDTO()
                {
                    AccessToken = token,
                    Message = string.IsNullOrEmpty(token) ? "Unable to generate token" : string.Empty
                };
            }
            else
                return new LoginResponseDTO()
                {
                    AccessToken = string.Empty,
                    Message = "Invalid password"
                };
#else
            var userSearchResponse = await _apiClient.ExecuteRequestAsync<SearchUserResponseDTO>(baseUrl: "",
                                                                                                 requestMethod: Method.Post,
                                                                                                 requestBody: searcUserRequest);



            if (userSearchResponse.IsSuccessful && userSearchResponse.Data is not null)
            {
                if (BCrypt.BCryptHelper.CheckPassword(requestData.Password, userSearchResponse.Data.HashPassword))
                {
                    var token = _tokenHandler.GenerateToken(userSearchResponse.Data);
                    return new LoginResponseDTO()
                    {
                        AccessToken = token,
                        Message = string.IsNullOrEmpty(token) ? "Unable to generate token" : string.Empty
                    };
                }
                else
                    return new LoginResponseDTO()
                    {
                        AccessToken = string.Empty,
                        Message = "Invalid password"
                    };
            }
            else
            {
                return new LoginResponseDTO()
                {
                    AccessToken = string.Empty,
                    Message = "User not found"
                };
            }
#endif
        }
    }
}

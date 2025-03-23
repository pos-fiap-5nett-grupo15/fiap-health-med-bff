using Fiap.Health.Med.Bff.Application.DTOs.Auth;
using Fiap.Health.Med.Bff.Application.Interfaces.Auth;
using Fiap.Health.Med.Bff.Infrastructure.Http.Interfaces;
using Fiap.Health.Med.Infra.DTOs.Auth;
using RestSharp;

namespace Fiap.Health.Med.Bff.Application.Handlers
{
    internal class AuthenticationHandler : IAuthenticationHandler
    {
        IApiClient _apiClient;
        ITokenHandler _tokenHandler;

        public AuthenticationHandler(IApiClient apiClient, ITokenHandler tokenHandler)
        {
            _apiClient = apiClient;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginResponseDTO> AuthenticateAsync(LoginRequestDTO requestData)
        {
            object searcUserRequest;

            if (!string.IsNullOrEmpty(requestData.Uf))
                searcUserRequest = new DoctorSearchByConcilDTO() { ConcilNumer = requestData.Username, Uf = requestData.Uf };
            else
                searcUserRequest = new PatientSearchByDocumentDTO() { Document = requestData.Username };

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
        }
    }
}

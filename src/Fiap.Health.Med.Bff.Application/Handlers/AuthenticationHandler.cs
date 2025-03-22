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

        public AuthenticationHandler(IApiClient apiClient)
        {
            _apiClient = apiClient;
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

            }
            else
            {
                return new LoginResponseDTO()
                {
                    AccessToken = string.Empty,
                    Message = "User not found"
                };
            }
            /* TO-DO:
             *  Verificar o tipo de usuário(médico ou paciente)
             *  Verificar se o usuário existe
             *  Verificar a senha do usuário
             *  Gerar o token
             */

            throw new NotImplementedException();
        }
    }
}

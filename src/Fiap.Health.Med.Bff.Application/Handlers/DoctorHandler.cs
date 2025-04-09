using BCrypt;
using Fiap.Health.Med.Bff.Application.Dtos.Auth.UserSearch;
using Fiap.Health.Med.Bff.Application.DTOs.Common;
using Fiap.Health.Med.Bff.Application.DTOs.Doctor.Create;
using Fiap.Health.Med.Bff.Application.Interfaces.Doctor;
using Fiap.Health.Med.Bff.CrossCutting.Settings;
using Fiap.Health.Med.Bff.Infrastructure.Http.Interfaces;
using Microsoft.Extensions.Options;
using RestSharp;

namespace Fiap.Health.Med.Bff.Application.Handlers
{
    public class DoctorHandler : IDoctorHandler
    {
        private readonly IApiClient _apiClient;
        private readonly ExternalServicesSettings _extenalApiSettings;

        public DoctorHandler(IApiClient apiClient, IOptions<ExternalServicesSettings> extenalApiSettings)
        {
            _apiClient = apiClient;
            _extenalApiSettings = extenalApiSettings.Value;
        }

        public async Task<HandlerResultDto> CreateNewDoctorAsync(CreateNewDoctorRequestDto requestData)
        {
            var result = await _apiClient.ExecuteRequestAsync(baseUrl: _extenalApiSettings.UserService.GetEndpoint("Doctor_CreateAsync"),
                                                              requestMethod: Method.Post,
                                                              requestBody: requestData);

            return new HandlerResultDto()
            {
                StatusCode = result.StatusCode,
                Success = result.IsSuccessful,
                ResponseData = result.Content,
                ErrorMessage = result.ErrorMessage
            };
        }

        public async Task<UserSearchResponseDto?> GetDoctorByConcilAsync(string uf, int crm)
        {
            var userSearchResponse = await _apiClient.ExecuteRequestAsync<UserSearchResponseDto>(baseUrl: _extenalApiSettings.UserService.GetEndpoint("Doctor_GetOneByConcilAsync"),
                                                                                                 requestMethod: Method.Get,
                                                                                                 resourceUrl: $"{uf}/{crm}");

            return userSearchResponse.Data;
        }
    }
}

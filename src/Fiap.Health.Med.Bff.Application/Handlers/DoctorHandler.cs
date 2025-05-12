using Fiap.Health.Med.Bff.Application.Dtos.Auth.UserSearch;
using Fiap.Health.Med.Bff.Application.DTOs.Common;
using Fiap.Health.Med.Bff.Application.DTOs.Doctor;
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

        public async Task<HandlerResultDto> CreateNewDoctorAsync(DoctorRequestDto requestData)
        {
            var result = await _apiClient.ExecuteRequestAsync(baseUrl: _extenalApiSettings.UserService.BaseURL,
                                                              resourceUrl: "Doctor",
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

        public async Task<HandlerResultDto?> DeleteDoctorAsync(int id)
        {
            var result = await _apiClient.ExecuteRequestAsync(baseUrl: _extenalApiSettings.UserService.BaseURL,
                                                              resourceUrl: $"Doctor/{id}",
                                                              requestMethod: Method.Delete);

            return new HandlerResultDto()
            {
                StatusCode = result.StatusCode,
                Success = result.IsSuccessful,
                ResponseData = result.Content,
                ErrorMessage = result.ErrorMessage
            };
        }

        public async Task<HandlerResultDto?> GetByIdDoctor(int id)
        {
            var result = await _apiClient.ExecuteRequestAsync(baseUrl: _extenalApiSettings.UserService.BaseURL,
                                                              requestMethod: Method.Get,
                                                              resourceUrl: $"Doctor/{id}");

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
            var userSearchResponse = await _apiClient.ExecuteRequestAsync<UserSearchResponseDto>(baseUrl: _extenalApiSettings.UserService.BaseURL,
                                                                                                 requestMethod: Method.Get,
                                                                                                 resourceUrl: $"Doctor/{uf}/{crm}");

            return userSearchResponse.Data;
        }

        public async Task<HandlerResultDto> PutDoctorAsync(int id, DoctorRequestDto requestData)
        {
            var result = await _apiClient.ExecuteRequestAsync(baseUrl: _extenalApiSettings.UserService.BaseURL,
                                                              requestMethod: Method.Put,
                                                              resourceUrl: $"Doctor/{id}", requestData);

            return new HandlerResultDto()
            {
                StatusCode = result.StatusCode,
                Success = result.IsSuccessful,
                ResponseData = result.Content,
                ErrorMessage = result.ErrorMessage
            };
        }
    }
}

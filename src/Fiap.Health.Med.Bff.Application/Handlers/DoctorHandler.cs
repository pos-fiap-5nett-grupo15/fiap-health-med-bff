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

        public async Task<HandlerResultDto?> DeleteDoctorAsync(int id)
        {
            var result = await _apiClient.ExecuteRequestAsync(baseUrl: _extenalApiSettings.UserService.GetEndpoint("Doctor_DeleteAsync"),
                                                                                                requestMethod: Method.Delete,
                                                                                                resourceUrl: $"{id}");

            return new HandlerResultDto()
            {
                StatusCode = result.StatusCode,
                Success = result.IsSuccessful,
                ResponseData = result.Content,
                ErrorMessage = result.ErrorMessage
            };
        }

        public async Task<HandlerResultDto?> GetAllDoctorAsync()
        {
            var result = await _apiClient.ExecuteRequestAsync(baseUrl: _extenalApiSettings.UserService.GetEndpoint("Doctor_GetAllAsync"),
                                                                                                 requestMethod: Method.Get,
                                                                                                 resourceUrl: null);

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
            var result = await _apiClient.ExecuteRequestAsync(baseUrl: _extenalApiSettings.UserService.GetEndpoint("Doctor_GetOneAsync"),
                                                                                                requestMethod: Method.Get,
                                                                                                resourceUrl: $"{id}");

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

        public async Task<HandlerResultDto> PutDoctorAsync(int id, DoctorRequestDto requestData)
        {
            var result = await _apiClient.ExecuteRequestAsync(baseUrl: _extenalApiSettings.UserService.GetEndpoint("Doctor_PutAsync"),
                                                                                                 requestMethod: Method.Put,
                                                                                                 resourceUrl: $"{id}", requestData);

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

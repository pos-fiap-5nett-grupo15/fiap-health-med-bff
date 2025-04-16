using Fiap.Health.Med.Bff.Application.Dtos.Auth.UserSearch;
using Fiap.Health.Med.Bff.Application.DTOs.Common;
using Fiap.Health.Med.Bff.Application.DTOs.Patient.Create;
using Fiap.Health.Med.Bff.Application.Interfaces.Patient;
using Fiap.Health.Med.Bff.CrossCutting.Settings;
using Fiap.Health.Med.Bff.Infrastructure.Http.Interfaces;
using Microsoft.Extensions.Options;
using RestSharp;

namespace Fiap.Health.Med.Bff.Application.Handlers
{
    public class PatientHandler : IPatientHandler
    {
        private readonly IApiClient _apiClient;
        private readonly ExternalServicesSettings _extenalApiSettings;

        public PatientHandler(IApiClient apiClient, IOptions<ExternalServicesSettings> extenalApiSettings)
        {
            _apiClient = apiClient;
            _extenalApiSettings = extenalApiSettings.Value;
        }

        public async Task<HandlerResultDto> CreateNewPatientAsync(CreateNewPatientRequestDto requestData)
        {
            var result = await _apiClient.ExecuteRequestAsync(baseUrl: _extenalApiSettings.UserService.GetEndpoint("Patient_CreateAsync"),
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

        public async Task<UserSearchResponseDto?> GetPatientByCpfAsync(int cpf)
        {
            var userSearchResponse = await _apiClient.ExecuteRequestAsync<UserSearchResponseDto>(baseUrl: _extenalApiSettings.UserService.GetEndpoint("Patient_CreateAsync"),
                                                                                     requestMethod: Method.Get,
                                                                                     resourceUrl: $"{cpf}");

            return userSearchResponse.Data;
        }
    }
}

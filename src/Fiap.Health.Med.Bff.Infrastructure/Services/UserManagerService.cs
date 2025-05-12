using Fiap.Health.Med.Bff.Infrastructure.Http.Interfaces;
using Fiap.Health.Med.Bff.Infrastructure.Http.Mappers;
using Fiap.Health.Med.Bff.Infrastructure.Http.ServiceRequestResponse;
using Microsoft.Extensions.Logging;

namespace Fiap.Health.Med.Bff.Infrastructure.Http.Services
{
    public class UserManagerService : IUserManagerService
    {
        private readonly ILogger<UserManagerService> _logger;
        private readonly IHttpClientUserManagerAPI _userManagerAPI;

        public UserManagerService(
            ILogger<UserManagerService> logger,
            IHttpClientUserManagerAPI userManagerAPI)
        {
            _logger = logger;
            _userManagerAPI = userManagerAPI;
        }

        public async Task<BaseServiceResponse> DeletePatientByIdAsync(int patientId, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(DeletePatientByIdAsync)}.");

            if (await _userManagerAPI.DeletePatientByIdAsync("authorization", patientId, ct) is var result && result is null || result.IsSuccess is false)
            {
                return new BaseServiceResponse()
                {
                    Success = false,
                    ErrorMessage = result?.Errors.FirstOrDefault() ?? $"{nameof(DeletePatientByIdAsync)} - An error ocurred while communicate with User Manager API with status code: '{result?.StatusCode}'."
                };
            }

            _logger.LogInformation($"{nameof(DeletePatientByIdAsync)} finished.");

            return new BaseServiceResponse { Success = true };
        }

        public async Task<BaseServiceResponse> UpdatePatientByIdAsync(int patientId, UpdatePatientByIdServiceRequest requestBody, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(UpdatePatientByIdAsync)}.");

            if (await _userManagerAPI.UpdatePatientByIdAsync("authorization", patientId, requestBody.MapToHttpRequest(), ct) is var result && result is null || result.IsSuccess is false)
            {
                return new BaseServiceResponse()
                {
                    Success = false,
                    ErrorMessage = result?.Errors.FirstOrDefault() ?? $"{nameof(UpdatePatientByIdAsync)} - An error ocurred while communicate with User Manager API with status code: '{result?.StatusCode}'."
                };
            }

            _logger.LogInformation($"{nameof(UpdatePatientByIdAsync)} finished.");

            return new BaseServiceResponse { Success = true };
        }

        public async Task<GetDoctorsByFiltersServiceResponse> GetDoctorsByFiltersAsync(GetDoctorsByFiltersServiceRequest requestBody, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(GetDoctorsByFiltersAsync)}.");

            if (await _userManagerAPI.GetDoctorsByFiltersAsync("authorization", requestBody.MapToHttpRequest(), ct) is var result && result is null || result.IsSuccess is false)
            {
                return new GetDoctorsByFiltersServiceResponse()
                {
                    Success = false,
                    ErrorMessage = result?.Errors.FirstOrDefault() ?? $"{nameof(GetDoctorsByFiltersAsync)} - An error ocurred while communicate with User Manager API with status code: '{result?.StatusCode}'."
                };
            }

            _logger.LogInformation($"{nameof(GetDoctorsByFiltersAsync)} finished.");

            return new GetDoctorsByFiltersServiceResponse
            { 
                Success = true,
                CurrentPage = result.CurrentPage,
                PageSize = result.PageSize,
                Doctors = result.Doctors,
                Total = result.Total
            };
        }
    }
}

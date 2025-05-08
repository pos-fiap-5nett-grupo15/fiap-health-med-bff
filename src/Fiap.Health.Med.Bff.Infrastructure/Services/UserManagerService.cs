using Fiap.Health.Med.Bff.Infrastructure.Http.HttpResponse;
using Fiap.Health.Med.Bff.Infrastructure.Http.Interfaces;
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
                    ErrorMessage = result?.Errors.FirstOrDefault() ?? $"{nameof(DeletePatientByIdAsync)} - An error ocurred while communicate with User Manager API."
                };
            }

            _logger.LogInformation($"{nameof(DeletePatientByIdAsync)} finished.");

            return new BaseServiceResponse { Success = true };
        }

        public async Task<BaseServiceResponse> UpdatePatientByIdAsync(int patientId, UpdatePatientByIdHttpRequest requestBody, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(UpdatePatientByIdAsync)}.");

            if (await _userManagerAPI.UpdatePatientByIdAsync("authorization", patientId, requestBody, ct) is var result && result is null || result.IsSuccess is false)
            {
                return new BaseServiceResponse()
                {
                    Success = false,
                    ErrorMessage = result?.Errors.FirstOrDefault() ?? $"{nameof(UpdatePatientByIdAsync)} - An error ocurred while communicate with User Manager API."
                };
            }

            _logger.LogInformation($"{nameof(UpdatePatientByIdAsync)} finished.");

            return new BaseServiceResponse { Success = true };
        }
    }
}

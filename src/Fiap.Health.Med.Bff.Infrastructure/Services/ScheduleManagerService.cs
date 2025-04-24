using Fiap.Health.Med.Bff.Infrastructure.Http.HttpResponse;
using Fiap.Health.Med.Bff.Infrastructure.Http.Interfaces;
using Microsoft.Extensions.Logging;

namespace Fiap.Health.Med.Bff.Infrastructure.Http.Services
{
    public class ScheduleManagerService : IScheduleManagerService
    {
        private readonly ILogger<ScheduleManagerService> _logger;
        private readonly IHttpClientScheduleManagerAPI _scheduleManagerAPI;

        public ScheduleManagerService(
            ILogger<ScheduleManagerService> logger,
            IHttpClientScheduleManagerAPI scheduleManagerAPI)
        {
            _logger = logger;
            _scheduleManagerAPI = scheduleManagerAPI;
        }

        public async Task<BaseServiceResponse> DeclineScheduleByIdAsync(long scheduleId, int doctorId, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(DeclineScheduleByIdAsync)}.");

            if (await _scheduleManagerAPI.DeclineScheduleByIdAsync("authorization", scheduleId, doctorId, ct) is var result && result is null || result.IsSuccess is false)
            {
                return new BaseServiceResponse()
                {
                    Success = false,
                    ErrorMessage = result?.Errors.FirstOrDefault() ?? $"{nameof(DeclineScheduleByIdAsync)} - An error ocurred while communicate with Schedule Manager API."
                };
            }

            _logger.LogInformation($"{nameof(DeclineScheduleByIdAsync)} finished.");

            return new BaseServiceResponse()
            {
                Success = true
            };
        }

        public async Task<BaseServiceResponse> AcceptScheduleByIdAsync(long scheduleId, int doctorId, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(AcceptScheduleByIdAsync)}.");

            if (await _scheduleManagerAPI.AcceptScheduleByIdAsync("authorization", scheduleId, doctorId, ct) is var result && result is null || result.IsSuccess is false)
            {
                return new BaseServiceResponse()
                {
                    Success = false,
                    ErrorMessage = result?.Errors.FirstOrDefault() ?? $"{nameof(AcceptScheduleByIdAsync)} - An error ocurred while communicate with Schedule Manager API."
                };
            }

            _logger.LogInformation($"{nameof(AcceptScheduleByIdAsync)} finished.");

            return new BaseServiceResponse()
            {
                Success = true
            };
        }
    }
}

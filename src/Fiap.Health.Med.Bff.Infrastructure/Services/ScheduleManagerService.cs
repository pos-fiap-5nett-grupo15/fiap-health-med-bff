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

        public async Task<BaseServiceResponse> UpdateScheduleByIdAsync(long scheduleId, int doctorId, DateTime scheduleDate, float schedulePrice, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(UpdateScheduleByIdAsync)}.");

            if (await _scheduleManagerAPI.UpdateScheduleByIdAsync("authorization", scheduleId, doctorId, scheduleDate, schedulePrice, ct) is var result && result is null || !result.IsSuccess)
            {
                return new BaseServiceResponse()
                {
                    Success = false,
                    ErrorMessage = result?.Errors.FirstOrDefault() ?? $"{nameof(UpdateScheduleByIdAsync)} - An error ocurred while communicate with Schedule Manager API."
                };
            }

            _logger.LogInformation($"{nameof(UpdateScheduleByIdAsync)} finished.");

            return new BaseServiceResponse()
            {
                Success = true
            };
        }

        public async Task<BaseServiceResponse> CreateScheduleAsync(int doctorId, DateTime scheduleTime, float price, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(CreateScheduleAsync)}.");

            if (await _scheduleManagerAPI.CreateScheduleAsync("authorization", doctorId, scheduleTime, price, ct) is var result && result is null || !result.IsSuccess)
            {
                return new BaseServiceResponse()
                {
                    Success = false,
                    ErrorMessage = result?.Errors.FirstOrDefault() ?? $"{nameof(CreateScheduleAsync)} - An error ocurred while communicate with Schedule Manager API."
                };
            }

            _logger.LogInformation($"{nameof(CreateScheduleAsync)} finished.");

            return new BaseServiceResponse()
            {
                Success = true
            };
        }
    }
}

﻿using Fiap.Health.Med.Bff.Infrastructure.Http.Interfaces;
using Fiap.Health.Med.Bff.Infrastructure.Http.ServiceRequestResponse;
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

        public async Task<GetScheduleServiceResponse> GetScheduleByIdAsync(long scheduleId, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(GetScheduleByIdAsync)}.");

            if (await _scheduleManagerAPI.GetScheduleByIdAsync("authorization", scheduleId, ct) is var result && result is null || !result.IsSuccess)
            {
                return new GetScheduleServiceResponse()
                {
                    Success = false,
                    ErrorMessage = result?.Errors.FirstOrDefault() ?? $"{nameof(GetScheduleByIdAsync)} - An error ocurred while communicate with Schedule Manager API."
                };
            }

            _logger.LogInformation($"{nameof(CreateScheduleAsync)} finished.");

            return new GetScheduleServiceResponse()
            {
                Success = true,
                Schedules = result.Schedules
            };
        }

        public async Task<GetScheduleServiceResponse> GetSchedulesByDoctorIdAsync(int doctorId, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(GetScheduleByIdAsync)}.");

            if (await _scheduleManagerAPI.GetSchedulesByDoctorIdAsync("authorization", doctorId, ct) is var result && result is null || !result.IsSuccess)
            {
                return new GetScheduleServiceResponse()
                {
                    Success = false,
                    ErrorMessage = result?.Errors.FirstOrDefault() ?? $"{nameof(GetSchedulesByDoctorIdAsync)} - An error ocurred while communicate with Schedule Manager API."
                };
            }

            _logger.LogInformation($"{nameof(CreateScheduleAsync)} finished.");

            return new GetScheduleServiceResponse()
            {
                Success = true,
                Schedules = result.Schedules
            };
        }

        public async Task<GetScheduleServiceResponse> GetSchedulesByPatientIdAsync(int patientId, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(GetScheduleByIdAsync)}.");

            if (await _scheduleManagerAPI.GetSchedulesByPatientIdAsync("authorization", patientId, ct) is var result && result is null || !result.IsSuccess)
            {
                return new GetScheduleServiceResponse()
                {
                    Success = false,
                    ErrorMessage = result?.Errors.FirstOrDefault() ?? $"{nameof(GetSchedulesByPatientIdAsync)} - An error ocurred while communicate with Schedule Manager API."
                };
            }

            _logger.LogInformation($"{nameof(CreateScheduleAsync)} finished.");

            return new GetScheduleServiceResponse()
            {
                Success = true,
                Schedules = result.Schedules
            };
        }

        public async Task<BaseServiceResponse> RequestScheduleToPatientAsync(long scheduleId, int patientId, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(RequestScheduleToPatientAsync)}.");

            if (await _scheduleManagerAPI.RequestScheduleToPatientAsync("authorization", scheduleId, patientId, ct) is var result && result is null || !result.IsSuccess)
            {
                return new BaseServiceResponse()
                {
                    Success = false,
                    ErrorMessage = result?.Errors.FirstOrDefault() ?? $"{nameof(CreateScheduleAsync)} - An error ocurred while communicate with Schedule Manager API."
                };
            }

            _logger.LogInformation($"{nameof(RequestScheduleToPatientAsync)} finished.");

            return new BaseServiceResponse()
            {
                Success = true
            };
        }

        public async Task<BaseServiceResponse> RequestPatientCancelScheduleAsync(long scheduleId, int patientId, string reason, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(RequestPatientCancelScheduleAsync)}.");

            if (await _scheduleManagerAPI.RequestPatientCancelScheduleAsync("authorization", scheduleId, patientId, reason, ct) is var result && result is null || !result.IsSuccess)
            {
                return new BaseServiceResponse()
                {
                    Success = false,
                    ErrorMessage = result?.Errors.FirstOrDefault() ?? $"{nameof(CreateScheduleAsync)} - An error ocurred while communicate with Schedule Manager API."
                };
            }

            _logger.LogInformation($"{nameof(RequestPatientCancelScheduleAsync)} finished.");

            return new BaseServiceResponse()
            {
                Success = true
            };
        }

        public async Task<GetScheduleServiceResponse> GetAllSchedulesAsync(CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(GetAllSchedulesAsync)}.");

            if (await _scheduleManagerAPI.GetAllSchedulesAsync("authorization", ct) is var result && result is null || !result.IsSuccess)
            {
                return new GetScheduleServiceResponse()
                {
                    Success = false,
                    ErrorMessage = result?.Errors.FirstOrDefault() ?? $"{nameof(GetAllSchedulesAsync)} - An error ocurred while communicate with Schedule Manager API."
                };
            }

            _logger.LogInformation($"{nameof(GetAllSchedulesAsync)} finished.");

            return new GetScheduleServiceResponse()
            {
                Success = true,
                Schedules = result.Schedules
            };
        }
    }
}

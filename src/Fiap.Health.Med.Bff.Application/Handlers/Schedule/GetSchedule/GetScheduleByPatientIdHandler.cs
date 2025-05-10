using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.GetSchedule.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.GetSchedule.Models;
using Fiap.Health.Med.Bff.Infrastructure.Http.Services;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.GetSchedule
{
    public class GetScheduleByPatientIdHandler : IGetScheduleByPatientIdHandler
    {
        private readonly ILogger<GetScheduleByPatientIdHandler> _logger;
        private readonly IScheduleManagerService _scheduleService;

        public GetScheduleByPatientIdHandler(
            ILogger<GetScheduleByPatientIdHandler> logger,
            IScheduleManagerService scheduleService)
        {
            _logger = logger;
            _scheduleService = scheduleService;
        }

        public async Task<Result<GetScheduleHandlerResponse>> HandlerAsync(int patientId, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(GetScheduleByPatientIdHandler)}.");

            if (patientId <= 0)
                return Result<GetScheduleHandlerResponse>.Fail(HttpStatusCode.BadRequest, "Invalid patient ID.");

            if (await _scheduleService.GetSchedulesByPatientIdAsync(patientId, ct) is var result && result is null || !result.Success)
                return Result<GetScheduleHandlerResponse>.Fail(HttpStatusCode.UnprocessableEntity, result.ErrorMessage);

            _logger.LogInformation($"{nameof(GetScheduleByPatientIdHandler)} finished.");
            return Result<GetScheduleHandlerResponse>.Success(HttpStatusCode.OK, new GetScheduleHandlerResponse() { Schedules = result.Schedules });
        }
    }
}

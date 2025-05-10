using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.GetSchedule.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.GetSchedule.Models;
using Fiap.Health.Med.Bff.Infrastructure.Http.Services;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.GetSchedule
{
    public class GetScheduleByDoctorIdHandler : IGetScheduleByDoctorIdHandler
    {
        private readonly ILogger<GetScheduleByDoctorIdHandler> _logger;
        private readonly IScheduleManagerService _scheduleService;

        public GetScheduleByDoctorIdHandler(
            ILogger<GetScheduleByDoctorIdHandler> logger,
            IScheduleManagerService scheduleService)
        {
            _logger = logger;
            _scheduleService = scheduleService;
        }

        public async Task<Result<GetScheduleHandlerResponse>> HandlerAsync(int doctorId, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(GetScheduleByDoctorIdHandler)}.");

            if (doctorId <= 0)
                return Result<GetScheduleHandlerResponse>.Fail(HttpStatusCode.BadRequest, "Invalid doctor ID.");

            if (await _scheduleService.GetSchedulesByDoctorIdAsync(doctorId, ct) is var result && result is null || !result.Success)
                return Result<GetScheduleHandlerResponse>.Fail(HttpStatusCode.UnprocessableEntity, result.ErrorMessage);

            _logger.LogInformation($"{nameof(GetScheduleByDoctorIdHandler)} finished.");
            return Result<GetScheduleHandlerResponse>.Success(HttpStatusCode.OK, new GetScheduleHandlerResponse() { Schedules = result.Schedules });
        }
    }
}

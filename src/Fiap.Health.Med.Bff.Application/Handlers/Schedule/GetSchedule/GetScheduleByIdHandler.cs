using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.GetSchedule.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.GetSchedule.Models;
using Fiap.Health.Med.Bff.Infrastructure.Http.Services;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.GetSchedule
{
    public class GetScheduleByIdHandler : IGetScheduleByIdHandler
    {
        private readonly ILogger<GetScheduleByIdHandler> _logger;
        private readonly IScheduleManagerService _scheduleService;

        public GetScheduleByIdHandler(
            ILogger<GetScheduleByIdHandler> logger,
            IScheduleManagerService scheduleService)
        {
            _logger = logger;
            _scheduleService = scheduleService;
        }

        public async Task<Result<GetScheduleHandlerResponse>> HandlerAsync(long scheduleId, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(GetScheduleByIdHandler)}.");

            if (scheduleId <= 0)
                return Result<GetScheduleHandlerResponse>.Fail(HttpStatusCode.BadRequest, "Invalid schedule ID.");

            if (await _scheduleService.GetScheduleByIdAsync(scheduleId, ct) is var result && result is null || !result.Success)
                return Result<GetScheduleHandlerResponse>.Fail(HttpStatusCode.UnprocessableEntity, result.ErrorMessage);

            _logger.LogInformation($"{nameof(GetScheduleByIdHandler)} finished.");
            return Result<GetScheduleHandlerResponse>.Success(HttpStatusCode.OK, new GetScheduleHandlerResponse() { Schedules = result.Schedules });
        }
    }
}

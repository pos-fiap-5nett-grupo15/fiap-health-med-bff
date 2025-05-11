using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.GetSchedule.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.GetSchedule.Models;
using Fiap.Health.Med.Bff.Infrastructure.Http.Services;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.GetSchedule
{
    public class GetAllSchedulesHandler : IGetAllSchedulesHandler
    {
        private readonly ILogger<GetAllSchedulesHandler> _logger;
        private readonly IScheduleManagerService _scheduleManagerService;

        public GetAllSchedulesHandler(
            ILogger<GetAllSchedulesHandler> logger,
            IScheduleManagerService scheduleManagerService)
        {
            _logger = logger;
            _scheduleManagerService = scheduleManagerService;
        }

        public async Task<Result<GetScheduleHandlerResponse>> HandlerAsync(CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(GetAllSchedulesHandler)}.");

            if (await _scheduleManagerService.GetAllSchedulesAsync(ct) is var result && result is null || !result.Success)
                return Result<GetScheduleHandlerResponse>.Fail(HttpStatusCode.UnprocessableEntity, result.ErrorMessage);

            _logger.LogInformation($"{nameof(GetAllSchedulesHandler)} finished.");
            return Result<GetScheduleHandlerResponse>.Success(HttpStatusCode.OK, new GetScheduleHandlerResponse() { Schedules = result.Schedules });
        }
    }
}

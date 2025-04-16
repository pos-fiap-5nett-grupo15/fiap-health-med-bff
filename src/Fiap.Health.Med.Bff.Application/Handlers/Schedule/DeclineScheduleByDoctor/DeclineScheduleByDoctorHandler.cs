using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.DTOs.Schedule.DeclineScheduleByDoctor;
using Fiap.Health.Med.Bff.Application.Interfaces.Schedule.DeclineScheduleByDoctor;
using Fiap.Health.Med.Bff.Infrastructure.Http.Services;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.DeclineScheduleByDoctor
{
    public class DeclineScheduleByDoctorHandler : IDeclineScheduleByDoctorHandler
    {
        private readonly IScheduleManagerService _scheduleManagerService;
        private readonly ILogger<DeclineScheduleByDoctorHandler> _logger;

        public DeclineScheduleByDoctorHandler(
            IScheduleManagerService scheduleManagerService,
            ILogger<DeclineScheduleByDoctorHandler> logger)
        {
            _logger = logger;
            _scheduleManagerService = scheduleManagerService;
        }

        public async Task<Result> HandlerAsync(DeclineScheduleByDoctorHandlerRequest request, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(DeclineScheduleByDoctorHandler)}.");

            if (await _scheduleManagerService.DeclineScheduleByIdAsync(request.ScheduleId, request.DoctorId, ct) is var result && result is null || !result.Success)
                return Result.Fail(HttpStatusCode.UnprocessableContent, result.ErrorMessage);

            _logger.LogInformation($"{nameof(DeclineScheduleByDoctorHandler)} finished.");
            return Result.Success(HttpStatusCode.NoContent);
        }
    }
}

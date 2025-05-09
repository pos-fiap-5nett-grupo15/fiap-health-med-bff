using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.AcceptScheduleByDoctor.Models;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.AcceptScheduleByDoctor.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.DeclineScheduleByDoctor;
using Fiap.Health.Med.Bff.Infrastructure.Http.Services;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.AcceptScheduleByDoctor
{
    public class AcceptScheduleByDoctorHandler : IAcceptScheduleByDoctorHandler
    {
        private readonly IScheduleManagerService _scheduleManagerService;
        private readonly ILogger<AcceptScheduleByDoctorHandler> _logger;

        public AcceptScheduleByDoctorHandler(
            IScheduleManagerService scheduleManagerService,
            ILogger<AcceptScheduleByDoctorHandler> logger)
        {
            _logger = logger;
            _scheduleManagerService = scheduleManagerService;
        }

        public async Task<Result> HandlerAsync(AcceptScheduleByDoctorHandlerRequest request, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(AcceptScheduleByDoctorHandler)}.");

            if (await _scheduleManagerService.AcceptScheduleByIdAsync(request.ScheduleId, request.DoctorId, ct) is var result && result is null || !result.Success)
                return Result.Fail(HttpStatusCode.UnprocessableContent, result.ErrorMessage);

            _logger.LogInformation($"{nameof(DeclineScheduleByDoctorHandler)} finished.");
            return Result.Success(HttpStatusCode.NoContent);
        }
    }
}

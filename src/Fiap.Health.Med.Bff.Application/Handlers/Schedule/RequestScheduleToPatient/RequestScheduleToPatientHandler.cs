using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.RequestScheduleToPatient.Interfaces;
using Fiap.Health.Med.Bff.Infrastructure.Http.Services;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.RequestScheduleToPatient
{
    public class RequestScheduleToPatientHandler : IRequestScheduleToPatientHandler
    {
        private readonly ILogger<RequestScheduleToPatientHandler> _logger;
        private readonly IScheduleManagerService _scheduleService;

        public RequestScheduleToPatientHandler(
            ILogger<RequestScheduleToPatientHandler> logger,
            IScheduleManagerService scheduleService)
        {
            _logger = logger;
            _scheduleService = scheduleService;
        }

        public async Task<Result> HandlerAsync(long scheduleId, int patientId, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(RequestScheduleToPatientHandler)}.");

            if (scheduleId <= 0)
                return Result.Fail(HttpStatusCode.BadRequest, "Invalid schedule id.");

            if (patientId <= 0)
                return Result.Fail(HttpStatusCode.BadRequest, "Invalid patient id.");

            if (await _scheduleService.RequestScheduleToPatientAsync(scheduleId, patientId, ct) is var result && result is null || !result.Success)
                return Result.Fail(HttpStatusCode.UnprocessableContent, result.ErrorMessage);

            _logger.LogInformation($"{nameof(RequestScheduleToPatientHandler)} finished.");
            return Result.Success(HttpStatusCode.NoContent);
        }
    }
}

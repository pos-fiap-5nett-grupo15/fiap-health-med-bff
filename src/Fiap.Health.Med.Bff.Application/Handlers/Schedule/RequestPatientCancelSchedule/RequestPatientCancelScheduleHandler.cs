using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.RequestPatientCancelSchedule.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.RequestPatientCancelSchedule.Models;
using Fiap.Health.Med.Bff.Infrastructure.Http.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.RequestPatientCancelSchedule
{
    public class RequestPatientCancelScheduleHandler : IRequestPatientCancelScheduleHandler
    {
        private readonly ILogger<RequestPatientCancelScheduleHandler> _logger;
        private readonly IScheduleManagerService _scheduleManagerService;
        private readonly IValidator<RequestPatientCancelScheduleHandlerRequest> _validator;

        public RequestPatientCancelScheduleHandler(
            ILogger<RequestPatientCancelScheduleHandler> logger,
            IScheduleManagerService scheduleManagerService,
            IValidator<RequestPatientCancelScheduleHandlerRequest> validator)
        {
            _logger = logger;
            _scheduleManagerService = scheduleManagerService;
            _validator = validator;
        }

        public async Task<Result> HandlerAsync(RequestPatientCancelScheduleHandlerRequest request, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(RequestPatientCancelScheduleHandler)}.");

            if (_validator.Validate(request) is var validationResult && !validationResult.IsValid)
                return Result.Fail(HttpStatusCode.BadRequest, validationResult.Errors?.FirstOrDefault()?.ErrorMessage ?? "Invalid request.");

            if (await _scheduleManagerService.RequestPatientCancelScheduleAsync(request.ScheduleId, request.PatientId, request.Reason, ct) is var result && result is null || !result.Success)
                return Result.Fail(HttpStatusCode.UnprocessableContent, result.ErrorMessage);

            _logger.LogInformation($"{nameof(RequestPatientCancelScheduleHandler)} finished.");

            return Result.Success(HttpStatusCode.NoContent);
        }
    }
}

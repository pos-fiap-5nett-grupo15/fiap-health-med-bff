using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.UpdateSchedule.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.UpdateSchedule.Models;
using Fiap.Health.Med.Bff.Infrastructure.Http.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.UpdateSchedule
{
    public class UpdateScheduleHandler : IUpdateScheduleHandler
    {
        private readonly ILogger<UpdateScheduleHandler> _logger;
        private readonly IScheduleManagerService _scheduleService;
        private readonly IValidator<UpdateScheduleHandlerRequest> _validator;

        public UpdateScheduleHandler(
            ILogger<UpdateScheduleHandler> logger,
            IScheduleManagerService scheduleService,
            IValidator<UpdateScheduleHandlerRequest> validator)
        {
            _logger = logger;
            _scheduleService = scheduleService;
            _validator = validator;
        }

        public async Task<Result> HandlerAsync(UpdateScheduleHandlerRequest request, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(UpdateScheduleHandler)}.");

            if (_validator.Validate(request) is var validationResult && !validationResult.IsValid)
                return Result.Fail(HttpStatusCode.BadRequest, validationResult.Errors?.FirstOrDefault()?.ErrorMessage ?? "Invalid request.");

            if (await _scheduleService.UpdateScheduleByIdAsync(request.ScheduleId, request.DoctorId, request.ScheduleTime, request.Price, ct) is var result && result is null || !result.Success)
                return Result.Fail(HttpStatusCode.UnprocessableContent, result.ErrorMessage);

            _logger.LogInformation($"{nameof(UpdateScheduleHandler)} finished.");
            return Result.Success(HttpStatusCode.NoContent);
        }
    }
}

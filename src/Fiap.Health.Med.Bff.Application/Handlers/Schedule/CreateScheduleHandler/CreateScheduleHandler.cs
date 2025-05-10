using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.CreateScheduleHandler.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.CreateScheduleHandler.Models;
using Fiap.Health.Med.Bff.Infrastructure.Http.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.CreateScheduleHandler
{
    public class CreateScheduleHandler : ICreateScheduleHandler
    {
        private readonly ILogger<CreateScheduleHandler> _logger;
        private readonly IScheduleManagerService _scheduleService;
        private readonly IValidator<CreateScheduleHandlerRequest> _validator;

        public CreateScheduleHandler(
            ILogger<CreateScheduleHandler> logger,
            IScheduleManagerService scheduleService,
            IValidator<CreateScheduleHandlerRequest> validator)
        {
            _logger = logger;
            _scheduleService = scheduleService;
            _validator = validator;
        }

        public async Task<Result> HandlerAsync(CreateScheduleHandlerRequest request, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(CreateScheduleHandler)}.");

            if (_validator.Validate(request) is var validationResult && !validationResult.IsValid)
                return Result.Fail(HttpStatusCode.BadRequest, validationResult.Errors?.FirstOrDefault()?.ErrorMessage ?? "Invalid request.");

            if (await _scheduleService.CreateScheduleAsync(request.DoctorId, request.ScheduleTime, request.Price, ct) is var result && result is null || !result.Success)
                return Result.Fail(HttpStatusCode.UnprocessableContent, result.ErrorMessage);

            _logger.LogInformation($"{nameof(CreateScheduleHandlerRequest)} finished.");
            return Result.Success(HttpStatusCode.Created);
        }
    }
}

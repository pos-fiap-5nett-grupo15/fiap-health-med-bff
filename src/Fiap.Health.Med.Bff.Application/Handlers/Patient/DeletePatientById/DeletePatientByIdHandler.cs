using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.Handlers.Patient.DeletePatientById.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Patient.DeletePatientById.Models;
using Fiap.Health.Med.Bff.Infrastructure.Http.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Fiap.Health.Med.Bff.Application.Handlers.Patient.DeletePatientById
{
    public class DeletePatientByIdHandler : IDeletePatientByIdHandler
    {
        private readonly ILogger<DeletePatientByIdHandler> _logger;
        private readonly IUserManagerService _userManagerService;
        private readonly IValidator<DeletePatientByIdHandlerRequest> _validator;

        public DeletePatientByIdHandler(
            ILogger<DeletePatientByIdHandler> logger,
            IUserManagerService userManagerService,
            IValidator<DeletePatientByIdHandlerRequest> validator)
        {
            _logger = logger;
            _validator = validator;
            _userManagerService = userManagerService;
        }

        public async Task<Result> HandlerAsync(DeletePatientByIdHandlerRequest request, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(DeletePatientByIdHandler)}.");

            if (_validator.Validate(request) is var validationResult && validationResult.IsValid is false)
                return Result.Fail(HttpStatusCode.BadRequest, validationResult.Errors?.FirstOrDefault()?.ErrorMessage ?? "Invalid request.");

            if (await _userManagerService.DeletePatientByIdAsync(request.PatientId, ct) is var result && result is null || !result.Success)
                return Result.Fail(HttpStatusCode.UnprocessableContent, result.ErrorMessage);

            _logger.LogInformation($"{nameof(DeletePatientByIdHandler)} finished.");
            return Result.Success(HttpStatusCode.NoContent);
        }
    }
}
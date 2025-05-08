using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.Handlers.Patient.UpdatePatientById.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Patient.UpdatePatientById.Models;
using Fiap.Health.Med.Bff.Infrastructure.Http.HttpResponse;
using Fiap.Health.Med.Bff.Infrastructure.Http.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Fiap.Health.Med.Bff.Application.Handlers.Patient.UpdatePatientById
{
    public class UpdatePatientByIdHandler : IUpdatePatientByIdHandler
    {
        private readonly ILogger<UpdatePatientByIdHandler> _logger;
        private readonly IUserManagerService _userManagerService;
        private readonly IValidator<UpdatePatientByIdHandlerRequest> _validator;

        public UpdatePatientByIdHandler(
            ILogger<UpdatePatientByIdHandler> logger,
            IUserManagerService userManagerService,
            IValidator<UpdatePatientByIdHandlerRequest> validator)
        {
            _logger = logger;
            _validator = validator;
            _userManagerService = userManagerService;
        }

        public async Task<Result> HandlerAsync(UpdatePatientByIdHandlerRequest request, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(UpdatePatientByIdHandler)}.");

            if (_validator.Validate(request) is var validationResult && validationResult.IsValid is false)
                return Result.Fail(HttpStatusCode.BadRequest, validationResult.Errors?.FirstOrDefault()?.ErrorMessage ?? "Invalid request.");

            if (await _userManagerService.UpdatePatientByIdAsync(request.PatientId, MapToHttpRequest(request), ct) is var result && result is null || !result.Success)
                return Result.Fail(HttpStatusCode.UnprocessableContent, result.ErrorMessage);

            _logger.LogInformation($"{nameof(UpdatePatientByIdHandler)} finished.");
            return Result.Success(HttpStatusCode.NoContent);
        }

        private static UpdatePatientByIdHttpRequest MapToHttpRequest(UpdatePatientByIdHandlerRequest request) =>
            new()
            {
                Name = request.Name,
                Document = request.Document,
                HashedPassword = request.Password, // TODO: Aplicar hash de senha
                Email = request.Email
            };
    }
}
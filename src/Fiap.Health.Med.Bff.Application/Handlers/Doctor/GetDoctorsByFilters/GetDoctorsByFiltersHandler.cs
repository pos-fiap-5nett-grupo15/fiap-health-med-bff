using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.Handlers.Doctor.GetDoctorsByFilters.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Doctor.GetDoctorsByFilters.Models;
using Fiap.Health.Med.Bff.Application.Mappers;
using Fiap.Health.Med.Bff.Infrastructure.Http.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Fiap.Health.Med.Bff.Application.Handlers.Doctor.GetDoctorsByFilters
{
    public class GetDoctorsByFiltersHandler : IGetDoctorsByFiltersHandler
    {
        private readonly ILogger<GetDoctorsByFiltersHandler> _logger;
        private readonly IUserManagerService _userManagerService;
        private readonly IValidator<GetDoctorsByFiltersHandlerRequest> _validator;

        public GetDoctorsByFiltersHandler(
            ILogger<GetDoctorsByFiltersHandler> logger,
            IUserManagerService userManagerService,
            IValidator<GetDoctorsByFiltersHandlerRequest> validator)
        {
            _logger = logger;
            _validator = validator;
            _userManagerService = userManagerService;
        }

        public async Task<Result<GetDoctorsByFiltersHandlerResponse>> HandlerAsync(GetDoctorsByFiltersHandlerRequest request, CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(GetDoctorsByFiltersHandler)}.");

            if (_validator.Validate(request) is var validationResult && validationResult.IsValid is false)
                return Result<GetDoctorsByFiltersHandlerResponse>.Fail(HttpStatusCode.BadRequest, validationResult.Errors?.FirstOrDefault()?.ErrorMessage ?? "Invalid request.");

            if (await _userManagerService.GetDoctorsByFiltersAsync(request.MapToServiceRequest(), ct) is var result && result is null || !result.Success)
                return Result<GetDoctorsByFiltersHandlerResponse>.Fail(HttpStatusCode.UnprocessableContent, result.ErrorMessage);

            _logger.LogInformation($"{nameof(GetDoctorsByFiltersHandler)} finished.");
            return Result<GetDoctorsByFiltersHandlerResponse>.Success(HttpStatusCode.OK, new GetDoctorsByFiltersHandlerResponse
            {
                CurrentPage = result.CurrentPage,
                PageSize = result.PageSize,
                Total = result.Total,
                Doctors = result.Doctors,
            });
        }
    }
}
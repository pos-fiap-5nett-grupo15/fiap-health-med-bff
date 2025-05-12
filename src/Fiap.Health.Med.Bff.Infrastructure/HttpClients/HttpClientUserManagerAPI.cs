using Fiap.Health.Med.Bff.Infrastructure.Http.HttpResponse;
using Fiap.Health.Med.Bff.Infrastructure.Http.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Fiap.Health.Med.Bff.Infrastructure.Http.HttpClients
{
    public class HttpClientUserManagerAPI : BaseHttpClient, IHttpClientUserManagerAPI
    {
        public HttpClientUserManagerAPI(
            HttpClient httpClient,
            ILogger<HttpClientUserManagerAPI> logger)
            : base(logger, httpClient, "User Manager API") { }

        public async Task<DeletePatientByIdHttpResponse?> DeletePatientByIdAsync(
            string authorization,
            int patientId,
            CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(DeletePatientByIdAsync)}");

            (var response, var statusCode) = await SendDeleteAsync<DeletePatientByIdHttpResponse?>($"patient/{patientId}", authorization, ct);

            _logger.LogInformation($"{nameof(DeletePatientByIdAsync)} finished.");

            if (statusCode is HttpStatusCode.OK ||
                statusCode is HttpStatusCode.NoContent)
                return new DeletePatientByIdHttpResponse
                {
                    IsSuccess = true
                };

            return response;
        }

        public async Task<UpdatePatientByIdHttpResponse?> UpdatePatientByIdAsync(
            string authorization,
            int patientId,
            UpdatePatientByIdHttpRequest requestBody,
            CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(UpdatePatientByIdAsync)}");

            (var response, var statusCode, _) = await SendPutAsync<UpdatePatientByIdHttpResponse?>(requestBody, $"patient/{patientId}", authorization, ct);

            _logger.LogInformation($"{nameof(UpdatePatientByIdAsync)} finished.");

            if (statusCode is HttpStatusCode.OK ||
                statusCode is HttpStatusCode.NoContent)
                return new UpdatePatientByIdHttpResponse
                {
                    IsSuccess = true
                };

            return response;
        }

        public async Task<GetDoctorsByFiltersHttpResponse?> GetDoctorsByFiltersAsync(
            string authorization,
            GetDoctorsByFiltersHttpRequest requestBody,
            CancellationToken ct)
        {
            _logger.LogInformation($"Starting {nameof(GetDoctorsByFiltersAsync)}");

            string queryParameter = "doctor/filters?";

            if (!string.IsNullOrWhiteSpace(requestBody.DoctorName))
                queryParameter += $"{nameof(requestBody.DoctorName)}={requestBody.DoctorName}&";
            if (!string.IsNullOrWhiteSpace(requestBody.DoctorCrmUf))
                queryParameter += $"{nameof(requestBody.DoctorCrmUf)}={requestBody.DoctorCrmUf}&";
            if (requestBody.DoctorDoncilNumber.HasValue)
                queryParameter += $"{nameof(requestBody.DoctorDoncilNumber)}={requestBody.DoctorDoncilNumber.Value}&";
            if (requestBody.DoctorSpecialty.HasValue)
                queryParameter += $"{nameof(requestBody.DoctorSpecialty)}={requestBody.DoctorSpecialty.Value}&";

            queryParameter += $"{nameof(requestBody.CurrentPage)}={requestBody.CurrentPage}&";
            queryParameter += $"{nameof(requestBody.PageSize)}={requestBody.PageSize}";

            (var response, var statusCode, _) = await SendGetAsync<GetDoctorsByFiltersHttpResponse?>(queryParameter, authorization, ct);

            _logger.LogInformation($"{nameof(GetDoctorsByFiltersAsync)} finished.");

            if (statusCode is HttpStatusCode.OK)
                return new GetDoctorsByFiltersHttpResponse
                {
                    IsSuccess = true,
                    CurrentPage = response.CurrentPage,
                    PageSize = response.PageSize,
                    Total = response.Total,
                    Doctors = response.Doctors
                };

            return response;
        }
    }
}

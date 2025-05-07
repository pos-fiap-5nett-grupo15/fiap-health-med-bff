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
    }
}

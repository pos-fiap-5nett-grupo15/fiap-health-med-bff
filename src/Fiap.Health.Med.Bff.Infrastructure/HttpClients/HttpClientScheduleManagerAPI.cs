using Fiap.Health.Med.Bff.Infrastructure.Http.HttpResponse;
using Fiap.Health.Med.Bff.Infrastructure.Http.Interfaces;
using Microsoft.Extensions.Logging;

namespace Fiap.Health.Med.Bff.Infrastructure.Http.HttpClients
{
    public class HttpClientScheduleManagerAPI : BaseHttpClient, IHttpClientScheduleManagerAPI
    {
        public HttpClientScheduleManagerAPI(
            HttpClient httpClient,
            ILogger<HttpClientScheduleManagerAPI> logger)
            : base(logger, httpClient, "Schedule Manager API") { }

        public async Task<DeclineScheduleByIdHttpResponse?> DeclineScheduleByIdAsync(
            string authorization,
            long scheduleId,
            int doctorId,
            CancellationToken ct)
        {
            (var response, _) = await SendPatchAsync<DeclineScheduleByIdHttpResponse?>(null, $"api/Schedule/{scheduleId}/decline/{doctorId}", authorization, ct);

            return response;
        }

        public async Task<DeclineScheduleByIdHttpResponse?> AcceptScheduleByIdAsync(
          string authorization,
          long scheduleId,
          int doctorId,
          CancellationToken ct)
        {
            (var response, _) = await SendPatchAsync<DeclineScheduleByIdHttpResponse?>(null, $"api/Schedule/{scheduleId}/accept/{doctorId}", authorization, ct);

            return response;
        }
    }
}

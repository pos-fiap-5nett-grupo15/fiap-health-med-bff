using Fiap.Health.Med.Bff.Infrastructure.Http.HttpResponse;
using Fiap.Health.Med.Bff.Infrastructure.Http.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Fiap.Health.Med.Bff.Infrastructure.Http.HttpClients
{
    public class HttpClientScheduleManagerAPI : IHttpClientScheduleManagerAPI
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<HttpClientScheduleManagerAPI> _logger;

        public HttpClientScheduleManagerAPI(
            HttpClient httpClient,
            ILogger<HttpClientScheduleManagerAPI> logger)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

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

        #region Private and public methods:
        private async Task<(T?, HttpStatusCode statusCode)> SendGetAsync<T>(
            string resourceRoute,
            string authorization,
            CancellationToken ct)
        {
            try
            {
                BuildHeader(authorization);
                var uri = $"{_httpClient.BaseAddress}{resourceRoute}";

                _logger.LogDebug($"Going to send request to Schedule Manager API resourse '{uri}'.");

                var httpResponse = await _httpClient.GetAsync(uri, ct);

                var rawResponse = await httpResponse.Content.ReadAsStringAsync(ct);

                _logger.LogDebug($"Schedule Manager API returned status code ({httpResponse.StatusCode}): '{rawResponse}'.");

                if (!string.IsNullOrWhiteSpace(rawResponse))
                {
                    return (JsonSerializer.Deserialize<T>(rawResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }), httpResponse.StatusCode);
                }

                return (default, httpResponse.StatusCode);

            }
            catch (Exception e)
            {
                _logger.LogCritical(e, $"Exception while trying to send request: {e.GetType()} - {e.Message} - {e.StackTrace}.");
                throw;
            }
            finally
            {
                _httpClient.DefaultRequestHeaders.Clear();
            }
        }

        private async Task<(T?, HttpStatusCode statusCode)> SendPostAsync<T>(
            object contentRequest,
            string resourceRoute,
            string authorization,
            CancellationToken ct)
        {
            try
            {
                BuildHeader(authorization);
                var uri = $"{_httpClient.BaseAddress}{resourceRoute}";

                HttpContent? requestBody = null;
                if (contentRequest is not null)
                {
                    requestBody = new StringContent(JsonSerializer.Serialize(contentRequest), Encoding.UTF8, "application/json");
                }

                _logger.LogDebug($"Going to send request to Schedule Manager API resourse '{uri}': {requestBody}");

                var httpResponse = await _httpClient.PatchAsync(uri, requestBody, ct);

                var rawResponse = await httpResponse.Content.ReadAsStringAsync(ct);

                _logger.LogDebug($"Schedule Manager API returned status code ({httpResponse.StatusCode}): '{rawResponse}'.");

                if (!string.IsNullOrWhiteSpace(rawResponse))
                {
                    return (JsonSerializer.Deserialize<T>(rawResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }), httpResponse.StatusCode);
                }

                return (default, httpResponse.StatusCode);

            }
            catch (Exception e)
            {
                _logger.LogCritical(e, $"Exception while trying to send request: {e.GetType()} - {e.Message} - {e.StackTrace}.");
                throw;
            }
            finally
            {
                _httpClient.DefaultRequestHeaders.Clear();
            }
        }

        private async Task<(T?, HttpStatusCode statusCode)> SendPatchAsync<T>(
            object? contentRequest,
            string resourceRoute,
            string authorization,
            CancellationToken ct)
        {
            try
            {
                BuildHeader(authorization);
                var uri = $"{_httpClient.BaseAddress}{resourceRoute}";

                HttpContent? requestBody = null;
                if (contentRequest is not null)
                {
                    requestBody = new StringContent(JsonSerializer.Serialize(contentRequest), Encoding.UTF8, "application/json");
                }

                _logger.LogDebug($"Going to send request to Schedule Manager API resourse '{uri}': {requestBody}");

                var httpResponse = await _httpClient.PatchAsync(uri, requestBody, ct);

                var rawResponse = await httpResponse.Content.ReadAsStringAsync(ct);

                _logger.LogDebug($"Schedule Manager API returned status code ({httpResponse.StatusCode}): '{rawResponse}'.");

                if (!string.IsNullOrWhiteSpace(rawResponse))
                {
                    return (JsonSerializer.Deserialize<T>(rawResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true}), httpResponse.StatusCode);
                }

                return (default, httpResponse.StatusCode);

            }
            catch (Exception e)
            {
                _logger.LogCritical(e, $"Exception while trying to send request: {e.GetType()} - {e.Message} - {e.StackTrace}.");
                throw;
            }
            finally
            {
                _httpClient.DefaultRequestHeaders.Clear();
            }
        }

        private void BuildHeader(string authorization)
        {
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorization);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        #endregion Private and public methods.
    }
}

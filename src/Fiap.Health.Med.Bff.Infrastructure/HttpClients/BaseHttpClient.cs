using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Fiap.Health.Med.Bff.Infrastructure.Http.HttpClients
{
    public abstract class BaseHttpClient
    {
        private readonly HttpClient _httpClient;
        public readonly ILogger _logger;
        public readonly string _serviceName;

        protected BaseHttpClient(
            ILogger logger,
            HttpClient httpClient,
            string serviceName)
        {
            _logger = logger;
            _httpClient = httpClient;
            _serviceName = serviceName;
        }

        public async Task<(T?, HttpStatusCode statusCode)> SendGetAsync<T>(
            string resourceRoute,
            string authorization,
            CancellationToken ct)
        {
            try
            {
                BuildHeader(authorization);
                var uri = $"{_httpClient.BaseAddress}{resourceRoute}";

                _logger.LogDebug($"Going to send request to {_serviceName} resourse '{uri}'.");

                var httpResponse = await _httpClient.GetAsync(uri, ct);

                var rawResponse = await httpResponse.Content.ReadAsStringAsync(ct);

                _logger.LogDebug($"{_serviceName} returned status code ({httpResponse.StatusCode}): '{rawResponse}'.");

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
                _httpClient.Dispose();
            }
        }

        public async Task<(T?, HttpStatusCode statusCode, string rawResponse)> SendPostAsync<T>(
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

                _logger.LogDebug($"Going to send request to {_serviceName} resourse '{uri}': {requestBody}");

                var httpResponse = await _httpClient.PostAsync(uri, requestBody, ct);

                var rawResponse = await httpResponse.Content.ReadAsStringAsync(ct);

                _logger.LogDebug($"{_serviceName} returned status code ({httpResponse.StatusCode}): '{rawResponse}'.");

                if (!string.IsNullOrWhiteSpace(rawResponse))
                {
                    try
                    {
                        return (JsonSerializer.Deserialize<T>(rawResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }), httpResponse.StatusCode, rawResponse);
                    }
                    catch (JsonException e)
                    {
                        return (default, httpResponse.StatusCode, rawResponse);
                    }
                }

                return (default, httpResponse.StatusCode, rawResponse);

            }
            catch (Exception e)
            {
                _logger.LogCritical(e, $"Exception while trying to send request: {e.GetType()} - {e.Message} - {e.StackTrace}.");
                throw;
            }
            finally
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.Dispose();
            }
        }

        public async Task<(T?, HttpStatusCode statusCode)> SendPatchAsync<T>(
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

                _logger.LogDebug($"Going to send request to {_serviceName} resourse '{uri}': {requestBody}");

                var httpResponse = await _httpClient.PatchAsync(uri, requestBody, ct);

                var rawResponse = await httpResponse.Content.ReadAsStringAsync(ct);

                _logger.LogDebug($"{_serviceName} returned status code ({httpResponse.StatusCode}): '{rawResponse}'.");

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
                _httpClient.Dispose();
            }
        }

        public async Task<(T?, HttpStatusCode statusCode, string? rawResponse)> SendPutAsync<T>(
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

                _logger.LogDebug($"Going to send request to {_serviceName} resourse '{uri}': {requestBody}");

                var httpResponse = await _httpClient.PutAsync(uri, requestBody, ct);

                var rawResponse = await httpResponse.Content.ReadAsStringAsync(ct);

                _logger.LogDebug($"{_serviceName} returned status code ({httpResponse.StatusCode}): '{rawResponse}'.");

                if (!string.IsNullOrWhiteSpace(rawResponse))
                {
                    try
                    {
                        return (JsonSerializer.Deserialize<T>(rawResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }), httpResponse.StatusCode, rawResponse);
                    }
                    catch (JsonException e)
                    {
                        return (default, httpResponse.StatusCode, rawResponse);
                    }
                }

                return (default, httpResponse.StatusCode, rawResponse);

            }
            catch (Exception e)
            {
                _logger.LogCritical(e, $"Exception while trying to send request: {e.GetType()} - {e.Message} - {e.StackTrace}.");
                throw;
            }
            finally
            {
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.Dispose();
            }
        }

        public async Task<(T?, HttpStatusCode statusCode)> SendDeleteAsync<T>(
            string resourceRoute,
            string authorization,
            CancellationToken ct)
        {
            try
            {
                BuildHeader(authorization);
                var uri = $"{_httpClient.BaseAddress}{resourceRoute}";

                _logger.LogDebug($"Going to send request to {_serviceName} resourse '{uri}'.");

                var httpResponse = await _httpClient.DeleteAsync(uri, ct);

                var rawResponse = await httpResponse.Content.ReadAsStringAsync(ct);

                _logger.LogDebug($"{_serviceName} returned status code ({httpResponse.StatusCode}): '{rawResponse}'.");

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
                _httpClient.Dispose();
            }
        }

        private void BuildHeader(string authorization)
        {
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authorization);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}

using Fiap.Health.Med.Bff.Infrastructure.Http.HttpResponse;
using Fiap.Health.Med.Bff.Infrastructure.Http.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.RegularExpressions;

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

        public async Task<UpdateScheduleHttpResponse?> UpdateScheduleByIdAsync(
            string authorization,
            long scheduleId,
            int doctorId,
            DateTime scheduleDate,
            float schedulePrice,
            CancellationToken ct)
        {
            (var response, var statusCode, var rawResponse) = await SendPutAsync<UpdateScheduleHttpResponse?>(
                new
                {
                    ScheduleTime = scheduleDate,
                    Price = schedulePrice
                },
                $"api/Schedule/{scheduleId}/{doctorId}",
                authorization,
                ct);

            if (statusCode is HttpStatusCode.OK ||
                statusCode is HttpStatusCode.NoContent)
            {
                return new UpdateScheduleHttpResponse
                {
                    IsSuccess = true
                };
            }
            else if ((response is null || response.Errors is null || response.Errors.Count() == 0) &&
                     !string.IsNullOrEmpty(rawResponse))
            {
                return new UpdateScheduleHttpResponse
                {
                    IsSuccess = false,
                    Errors = new List<string> { Regex.Replace(rawResponse, @"[{}\[\]\""]", string.Empty) }
                };
            }
            else
                return response;
        }

        public async Task<CreateScheduleHttpResponse> CreateScheduleAsync(
            string authorization,
            int doctorId,
            DateTime scheduleDate,
            float schedulePrice,
            CancellationToken ct)
        {
            (var response, var statusCode, var rawResponse) = await SendPostAsync<CreateScheduleHttpResponse>(
                new
                {
                    DoctorId = doctorId,
                    ScheduleTime = scheduleDate,
                    Price = schedulePrice
                },
                $"api/Schedule",
                authorization,
                ct);

            if (statusCode is HttpStatusCode.OK ||
               statusCode is HttpStatusCode.NoContent ||
               statusCode is HttpStatusCode.Created)
            {
                return new CreateScheduleHttpResponse
                {
                    IsSuccess = true
                };
            }
            else if ((response is null || response.Errors is null || response.Errors.Count() == 0) &&
                     !string.IsNullOrEmpty(rawResponse))
            {
                return new CreateScheduleHttpResponse
                {
                    IsSuccess = false,
                    Errors = new List<string> { Regex.Replace(rawResponse, @"[{}\[\]\""]", string.Empty) }
                };
            }
            else
            {
                return response;
            }
        }
    }
}

using Fiap.Health.Med.Bff.Domain.Dtos;
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
            (var response, var statusCode, var rawResponse) = await SendPatchAsync<DeclineScheduleByIdHttpResponse?>(null, $"api/Schedule/{scheduleId}/doctor/{doctorId}/decline", authorization, ct);

            if (statusCode is HttpStatusCode.OK ||
                statusCode is HttpStatusCode.NoContent)
            {
                return new DeclineScheduleByIdHttpResponse
                {
                    IsSuccess = true
                };
            }
            else if ((response is null || response.Errors is null || response.Errors.Count() == 0) &&
                     !string.IsNullOrEmpty(rawResponse))
            {
                return new DeclineScheduleByIdHttpResponse
                {
                    IsSuccess = false,
                    Errors = new List<string> { Regex.Replace(rawResponse, @"[{}\[\]\""]", string.Empty) }
                };
            }
            else
                return response;
        }

        public async Task<DeclineScheduleByIdHttpResponse?> AcceptScheduleByIdAsync(
          string authorization,
          long scheduleId,
          int doctorId,
          CancellationToken ct)
        {
            (var response, var statusCode, var rawResponse) = await SendPatchAsync<DeclineScheduleByIdHttpResponse?>(null, $"api/Schedule/{scheduleId}/doctor/{doctorId}/accept", authorization, ct);

            if (statusCode is HttpStatusCode.OK ||
                statusCode is HttpStatusCode.NoContent)
            {
                return new DeclineScheduleByIdHttpResponse
                {
                    IsSuccess = true
                };
            }
            else if ((response is null || response.Errors is null || response.Errors.Count() == 0) &&
                     !string.IsNullOrEmpty(rawResponse))
            {
                return new DeclineScheduleByIdHttpResponse
                {
                    IsSuccess = false,
                    Errors = new List<string> { Regex.Replace(rawResponse, @"[{}\[\]\""]", string.Empty) }
                };
            }
            else
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
                $"api/Schedule/{scheduleId}/doctor/{doctorId}/update",
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

        public async Task<GetScheduleHttpResponse> GetScheduleByIdAsync(
                    string authorization,
                    long scheduleId,
                    CancellationToken ct)
        {
            (var response, var statusCode, var rawResponse) = await SendGetAsync<GetScheduleResponse>($"api/Schedule/{scheduleId}", authorization, ct);

            if (statusCode is HttpStatusCode.OK ||
                statusCode is HttpStatusCode.NoContent ||
                statusCode is HttpStatusCode.Created ||
                statusCode is HttpStatusCode.NotFound)
            {
                return new GetScheduleHttpResponse
                {
                    IsSuccess = true,
                    Schedules = response is not null ? new List<GetScheduleResponse> { response } : new List<GetScheduleResponse>()
                };
            }
            else if (!string.IsNullOrEmpty(rawResponse))
            {
                return new GetScheduleHttpResponse
                {
                    IsSuccess = false,
                    Errors = new List<string> { Regex.Replace(rawResponse, @"[{}\[\]\""]", string.Empty) }
                };
            }
            else
            {
                return new GetScheduleHttpResponse
                {
                    IsSuccess = false
                };
            }
        }

        public async Task<GetScheduleHttpResponse> GetSchedulesByDoctorIdAsync(
            string authorization,
            int doctorId,
            CancellationToken ct)
        {
            (var response, var statusCode, var rawResponse) = await SendGetAsync<IEnumerable<GetScheduleResponse>>($"api/Schedule/doctor/{doctorId}", authorization, ct);

            if (statusCode is HttpStatusCode.OK ||
                statusCode is HttpStatusCode.NoContent ||
                statusCode is HttpStatusCode.Created ||
                statusCode is HttpStatusCode.NotFound)
            {
                return new GetScheduleHttpResponse
                {
                    IsSuccess = true,
                    Schedules = response is not null ? response : new List<GetScheduleResponse>()
                };
            }
            else if (!string.IsNullOrEmpty(rawResponse))
            {
                return new GetScheduleHttpResponse
                {
                    IsSuccess = false,
                    Errors = new List<string> { Regex.Replace(rawResponse, @"[{}\[\]\""]", string.Empty) }
                };
            }
            else
            {
                return new GetScheduleHttpResponse
                {
                    IsSuccess = false
                };
            }
        }

        public async Task<GetScheduleHttpResponse> GetSchedulesByPatientIdAsync(
            string authorization,
            int patientId,
            CancellationToken ct)
        {
            (var response, var statusCode, var rawResponse) = await SendGetAsync<IEnumerable<GetScheduleResponse>>($"api/Schedule/patient/{patientId}", authorization, ct);

            if (statusCode is HttpStatusCode.OK ||
                statusCode is HttpStatusCode.NoContent ||
                statusCode is HttpStatusCode.Created ||
                statusCode is HttpStatusCode.NotFound)
            {
                return new GetScheduleHttpResponse
                {
                    IsSuccess = true,
                    Schedules = response is not null ? response : new List<GetScheduleResponse>()
                };
            }
            else if (!string.IsNullOrEmpty(rawResponse))
            {
                return new GetScheduleHttpResponse
                {
                    IsSuccess = false,
                    Errors = new List<string> { Regex.Replace(rawResponse, @"[{}\[\]\""]", string.Empty) }
                };
            }
            else
            {
                return new GetScheduleHttpResponse
                {
                    IsSuccess = false
                };
            }
        }

        public async Task<RequestScheduleToPatientHttpResponse> RequestScheduleToPatientAsync(
            string authorization,
            long scheduleId,
            int patientId,
            CancellationToken ct)
        {
            (var response, var statusCode, var rawResponse) = await SendPostAsync<RequestScheduleToPatientHttpResponse?>(null, $"api/Schedule/{scheduleId}/patient/{patientId}/request-schedule", authorization, ct);

            if (statusCode is HttpStatusCode.OK ||
                statusCode is HttpStatusCode.NoContent)
            {
                return new RequestScheduleToPatientHttpResponse
                {
                    IsSuccess = true
                };
            }
            else if ((response is null || response.Errors is null || response.Errors.Count() == 0) &&
                     !string.IsNullOrEmpty(rawResponse))
            {
                return new RequestScheduleToPatientHttpResponse
                {
                    IsSuccess = false,
                    Errors = new List<string> { Regex.Replace(rawResponse, @"[{}\[\]\""]", string.Empty) }
                };
            }
            else
                return response;
        }

        public async Task<RequestPatientCancelScheduleHttpResponse> RequestPatientCancelScheduleAsync(
            string authorization,
            long scheduleId,
            int patientId,
            string cancelReason,
            CancellationToken ct)
        {
            (var response, var statusCode, var rawResponse) = await SendPatchAsync<RequestPatientCancelScheduleHttpResponse?>(
                new
                {
                    Reason = cancelReason
                },
                $"api/Schedule/{scheduleId}/patient/{patientId}/cancel",
                authorization,
                ct);

            if (statusCode is HttpStatusCode.OK ||
                statusCode is HttpStatusCode.NoContent)
            {
                return new RequestPatientCancelScheduleHttpResponse
                {
                    IsSuccess = true
                };
            }
            else if ((response is null || response.Errors is null || response.Errors.Count() == 0) &&
                     !string.IsNullOrEmpty(rawResponse))
            {
                return new RequestPatientCancelScheduleHttpResponse
                {
                    IsSuccess = false,
                    Errors = new List<string> { Regex.Replace(rawResponse, @"[{}\[\]\""]", string.Empty) }
                };
            }
            else
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
                $"api/Schedule/{scheduleId}/doctor/{doctorId}/update",
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

        public async Task<GetScheduleHttpResponse> GetScheduleByIdAsync(
                    string authorization,
                    long scheduleId,
                    CancellationToken ct)
        {
            (var response, var statusCode, var rawResponse) = await SendGetAsync<GetScheduleResponse>($"api/Schedule/{scheduleId}", authorization, ct);

            if (statusCode is HttpStatusCode.OK ||
                statusCode is HttpStatusCode.NoContent ||
                statusCode is HttpStatusCode.Created ||
                statusCode is HttpStatusCode.NotFound)
            {
                return new GetScheduleHttpResponse
                {
                    IsSuccess = true,
                    Schedules = response is not null ? new List<GetScheduleResponse> { response } : new List<GetScheduleResponse>()
                };
            }
            else if (!string.IsNullOrEmpty(rawResponse))
            {
                return new GetScheduleHttpResponse
                {
                    IsSuccess = false,
                    Errors = new List<string> { Regex.Replace(rawResponse, @"[{}\[\]\""]", string.Empty) }
                };
            }
            else
            {
                return new GetScheduleHttpResponse
                {
                    IsSuccess = false
                };
            }
        }

        public async Task<GetScheduleHttpResponse> GetSchedulesByDoctorIdAsync(
            string authorization,
            int doctorId,
            CancellationToken ct)
        {
            (var response, var statusCode, var rawResponse) = await SendGetAsync<IEnumerable<GetScheduleResponse>>($"api/Schedule/doctor/{doctorId}", authorization, ct);

            if (statusCode is HttpStatusCode.OK ||
                statusCode is HttpStatusCode.NoContent ||
                statusCode is HttpStatusCode.Created ||
                statusCode is HttpStatusCode.NotFound)
            {
                return new GetScheduleHttpResponse
                {
                    IsSuccess = true,
                    Schedules = response is not null ? response : new List<GetScheduleResponse>()
                };
            }
            else if (!string.IsNullOrEmpty(rawResponse))
            {
                return new GetScheduleHttpResponse
                {
                    IsSuccess = false,
                    Errors = new List<string> { Regex.Replace(rawResponse, @"[{}\[\]\""]", string.Empty) }
                };
            }
            else
            {
                return new GetScheduleHttpResponse
                {
                    IsSuccess = false
                };
            }
        }

        public async Task<GetScheduleHttpResponse> GetSchedulesByPatientIdAsync(
            string authorization,
            int patientId,
            CancellationToken ct)
        {
            (var response, var statusCode, var rawResponse) = await SendGetAsync<IEnumerable<GetScheduleResponse>>($"api/Schedule/patient/{patientId}", authorization, ct);

            if (statusCode is HttpStatusCode.OK ||
                statusCode is HttpStatusCode.NoContent ||
                statusCode is HttpStatusCode.Created ||
                statusCode is HttpStatusCode.NotFound)
            {
                return new GetScheduleHttpResponse
                {
                    IsSuccess = true,
                    Schedules = response is not null ? response : new List<GetScheduleResponse>()
                };
            }
            else if (!string.IsNullOrEmpty(rawResponse))
            {
                return new GetScheduleHttpResponse
                {
                    IsSuccess = false,
                    Errors = new List<string> { Regex.Replace(rawResponse, @"[{}\[\]\""]", string.Empty) }
                };
            }
            else
            {
                return new GetScheduleHttpResponse
                {
                    IsSuccess = false
                };
            }
        }

        public async Task<GetScheduleHttpResponse> GetAllSchedulesAsync(string authorization, CancellationToken ct)
        {
            (var response, var statusCode, var rawResponse) = await SendGetAsync<IEnumerable<GetScheduleResponse>>($"api/Schedule", authorization, ct);

            if (statusCode is HttpStatusCode.OK ||
                statusCode is HttpStatusCode.NoContent ||
                statusCode is HttpStatusCode.Created ||
                statusCode is HttpStatusCode.NotFound)
            {
                return new GetScheduleHttpResponse
                {
                    IsSuccess = true,
                    Schedules = response is not null ? response : new List<GetScheduleResponse>()
                };
            }
            else if (!string.IsNullOrEmpty(rawResponse))
            {
                return new GetScheduleHttpResponse
                {
                    IsSuccess = false,
                    Errors = new List<string> { Regex.Replace(rawResponse, @"[{}\[\]\""]", string.Empty) }
                };
            }
            else
            {
                return new GetScheduleHttpResponse
                {
                    IsSuccess = false
                };
            }
        }
    }
}

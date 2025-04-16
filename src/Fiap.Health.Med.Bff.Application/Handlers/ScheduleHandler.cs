using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.DTOs.Common;
using Fiap.Health.Med.Bff.Application.DTOs.Schedule;
using Fiap.Health.Med.Bff.Application.Interfaces.Schedule;
using Fiap.Health.Med.Bff.CrossCutting.Settings;
using Fiap.Health.Med.Bff.Infrastructure.Http.Interfaces;
using Microsoft.Extensions.Options;
using RestSharp;

namespace Fiap.Health.Med.Bff.Application.Handlers
{
    public class ScheduleHandler : IScheduleHandler
    {
        private readonly IApiClient _apiClient;
        private readonly ExternalServicesSettings _extenalApiSettings;

        public ScheduleHandler(IApiClient apiClient,
                               IOptions<ExternalServicesSettings> externalServicesSettings)
        {
            _apiClient = apiClient;
            _extenalApiSettings = externalServicesSettings.Value;
        }

        public async Task<HandlerResultDto> UpdateScheduleAsync(UpdateScheduleRequestDto requestData)
        {
            var result = await _apiClient.ExecuteRequestAsync(baseUrl: _extenalApiSettings.ScheduleService.GetEndpoint("Schedule_UpdateAsync"),
                                                              requestMethod: Method.Put,
                                                              requestBody: requestData,
                                                              resourceUrl: requestData.Id.ToString());

            return new HandlerResultDto
            {
                StatusCode = result.StatusCode,
                Success = result.IsSuccessful,
                ResponseData = result.Content,
                ErrorMessage = result.ErrorMessage
            };
        }
    }
}

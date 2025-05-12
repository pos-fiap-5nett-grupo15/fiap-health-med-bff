using RestSharp;

namespace Fiap.Health.Med.Bff.Infrastructure.Http.Interfaces
{
    public interface IApiClient
    {
        Task<RestResponse<T>> ExecuteRequestAsync<T>(string baseUrl,
                                       Method requestMethod,
                                       string? resourceUrl = null,
                                       object? requestBody = null,
                                       string? authorizationToken = null,
                                       string? authorizationHeaderName = "Authorization",
                                       Dictionary<string, string>? headerParams = null);
        Task<RestResponse> ExecuteRequestAsync(string baseUrl,
                                          Method requestMethod,
                                          string? resourceUrl = null,
                                          object? requestBody = null,
                                          string? authorizationToken = null,
                                          string? authorizationHeaderName = "Authorization",
                                          Dictionary<string, string>? headerParams = null);
    }
}

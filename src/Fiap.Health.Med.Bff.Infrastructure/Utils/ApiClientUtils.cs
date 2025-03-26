using Fiap.Health.Med.Bff.Infrastructure.Http.Interfaces;
using RestSharp;

namespace Fiap.Health.Med.Bff.Infrastructure.Http.Utils
{
    public class ApiClientUtils : IApiClient
    {
        private RestRequest GenerateRequest(Method requestMethod,
                                            string? resourceUrl = null,
                                            object? requestBody = null,
                                            string? authorizationToken = null,
                                            string? authorizationHeaderName = "Authorization",
                                            Dictionary<string, string>? headerParams = null)
        {
            RestRequest request = new RestRequest(resourceUrl, requestMethod);

            if (!string.IsNullOrEmpty(authorizationToken))
            {
                request.AddHeader(authorizationHeaderName ?? "Authorization", authorizationToken);
            }
            if (headerParams is not null)
            {
                foreach (var header in headerParams)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }
            if (requestBody is not null)
            {
                request.AddBody(requestBody);
            }

            return request;
        }
        public async Task<RestResponse> ExecuteRequestAsync(string baseUrl,
                                                            Method requestMethod,
                                                            string? resourceUrl = null,
                                                            object? requestBody = null,
                                                            string? authorizationToken = null,
                                                            string? authorizationHeaderName = "Authorization",
                                                            Dictionary<string, string>? headerParams = null)
        {
            using (var client = new RestClient(baseUrl))
            {
                var request = GenerateRequest(requestMethod, resourceUrl, requestBody, authorizationToken, authorizationHeaderName, headerParams);
                return await client.ExecuteAsync(request);
            }
        }
        public async Task<RestResponse<T>> ExecuteRequestAsync<T>(string baseUrl,
                                                                    Method requestMethod,
                                                                    string? resourceUrl = null,
                                                                    object? requestBody = null,
                                                                    string? authorizationToken = null,
                                                                    string? authorizationHeaderName = "Authorization",
                                                                    Dictionary<string, string>? headerParams = null)
        {
            using (var client = new RestClient(baseUrl))
            {
                var request = GenerateRequest(requestMethod, resourceUrl, requestBody, authorizationToken, authorizationHeaderName, headerParams);
                return await client.ExecuteAsync<T>(request);
            }
        }
    }
}

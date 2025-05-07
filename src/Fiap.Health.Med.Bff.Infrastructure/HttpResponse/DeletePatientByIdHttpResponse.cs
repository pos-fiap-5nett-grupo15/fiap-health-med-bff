using System.Net;

namespace Fiap.Health.Med.Bff.Infrastructure.Http.HttpResponse
{
    public class DeletePatientByIdHttpResponse
    {
        public bool IsSuccess { get; init; }
        public HttpStatusCode StatusCode { get; init; }
        public List<string> Errors { get; init; } = [];
    }
}

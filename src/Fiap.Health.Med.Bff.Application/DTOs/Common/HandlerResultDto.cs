using System.Net;

namespace Fiap.Health.Med.Bff.Application.DTOs.Common
{
    public class HandlerResultDto
    {
        public bool Success { get; set; }
        public object? ResponseData { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

namespace Fiap.Health.Med.Bff.Infrastructure.Http.HttpResponse
{
    public class BaseServiceResponse
    {
        public bool Success { get; init; }
        public string ErrorMessage { get; init; } = string.Empty;
    }
}

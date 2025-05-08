using Fiap.Health.Med.Bff.Infrastructure.Http.HttpResponse;

namespace Fiap.Health.Med.Bff.Infrastructure.Http.Interfaces
{
    public interface IHttpClientUserManagerAPI
    {
        Task<DeletePatientByIdHttpResponse?> DeletePatientByIdAsync(
            string authorization,
            int patientId,
            CancellationToken ct);

        Task<UpdatePatientByIdHttpResponse?> UpdatePatientByIdAsync(
            string authorization,
            int patientId,
            UpdatePatientByIdHttpRequest requestBody,
            CancellationToken ct);
    }
}

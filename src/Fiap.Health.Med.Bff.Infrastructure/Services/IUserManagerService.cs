using Fiap.Health.Med.Bff.Infrastructure.Http.HttpResponse;

namespace Fiap.Health.Med.Bff.Infrastructure.Http.Services
{
    public interface IUserManagerService
    {
        Task<BaseServiceResponse> DeletePatientByIdAsync(int patientId, CancellationToken ct);

        Task<BaseServiceResponse> UpdatePatientByIdAsync(int patientId, UpdatePatientByIdHttpRequest requestBody, CancellationToken ct);
    }
}

using Fiap.Health.Med.Bff.Infrastructure.Http.ServiceRequestResponse;

namespace Fiap.Health.Med.Bff.Infrastructure.Http.Services
{
    public interface IUserManagerService
    {
        Task<BaseServiceResponse> DeletePatientByIdAsync(int patientId, CancellationToken ct);
        Task<BaseServiceResponse> UpdatePatientByIdAsync(int patientId, UpdatePatientByIdServiceRequest requestBody, CancellationToken ct);
        Task<GetDoctorsByFiltersServiceResponse> GetDoctorsByFiltersAsync(GetDoctorsByFiltersServiceRequest requestBody, CancellationToken ct);
    }
}

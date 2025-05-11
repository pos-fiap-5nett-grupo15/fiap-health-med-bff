using Fiap.Health.Med.Bff.Domain.Enums;

namespace Fiap.Health.Med.Bff.Infrastructure.Http.ServiceRequestResponse
{
    public class GetDoctorsByFiltersServiceRequest
    {
        public string? DoctorName { get; init; }
        public string? DoctorCrmUf { get; init; }
        public int? DoctorDoncilNumber { get; init; }
        public EMedicalSpecialty? DoctorSpecialty { get; init; }
        public int CurrentPage { get; init; }
        public int PageSize { get; init; }
    }
}

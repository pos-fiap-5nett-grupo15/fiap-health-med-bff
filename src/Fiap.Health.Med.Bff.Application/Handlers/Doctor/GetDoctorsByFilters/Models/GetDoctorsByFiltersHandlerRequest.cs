using Fiap.Health.Med.Bff.Domain.Enums;

namespace Fiap.Health.Med.Bff.Application.Handlers.Doctor.GetDoctorsByFilters.Models
{
    public class GetDoctorsByFiltersHandlerRequest
    {
        public string? DoctorName { get; init; }
        public string? DoctorCrmUf { get; init; }
        public int? DoctorDoncilNumber { get; init; }
        public EMedicalSpecialty? DoctorSpecialty { get; init; }
        public int CurrentPage { get; init; }
        public int PageSize { get; init; }
    }
}

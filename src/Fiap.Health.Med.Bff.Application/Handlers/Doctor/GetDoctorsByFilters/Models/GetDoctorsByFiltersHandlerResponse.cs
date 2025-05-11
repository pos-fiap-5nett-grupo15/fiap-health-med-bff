using Fiap.Health.Med.Bff.Domain.Models;

namespace Fiap.Health.Med.Bff.Application.Handlers.Doctor.GetDoctorsByFilters.Models
{
    public class GetDoctorsByFiltersHandlerResponse
    {
        public IEnumerable<DoctorEntity> Doctors { get; init; } = [];
        public int CurrentPage { get; init; }
        public int PageSize { get; init; }
        public int Total { get; init; }
    }
}

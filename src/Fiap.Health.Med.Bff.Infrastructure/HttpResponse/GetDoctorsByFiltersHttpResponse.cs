using Fiap.Health.Med.Bff.Domain.Models;

namespace Fiap.Health.Med.Bff.Infrastructure.Http.HttpResponse
{
    public class GetDoctorsByFiltersHttpResponse : BaseHttpResponse
    {
        public IEnumerable<DoctorEntity> Doctors { get; init; } = [];
        public int CurrentPage { get; init; }
        public int PageSize { get; init; }
        public int Total { get; init; }
    }
}

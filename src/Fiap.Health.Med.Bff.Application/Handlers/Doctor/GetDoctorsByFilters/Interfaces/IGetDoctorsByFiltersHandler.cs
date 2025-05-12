using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.Handlers.Doctor.GetDoctorsByFilters.Models;

namespace Fiap.Health.Med.Bff.Application.Handlers.Doctor.GetDoctorsByFilters.Interfaces
{
    public interface IGetDoctorsByFiltersHandler
    {
        Task<Result<GetDoctorsByFiltersHandlerResponse>> HandlerAsync(GetDoctorsByFiltersHandlerRequest request, CancellationToken ct);
    }
}

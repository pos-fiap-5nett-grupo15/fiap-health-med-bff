using Fiap.Health.Med.Bff.Application.Handlers.Doctor.GetDoctorsByFilters.Models;
using Fiap.Health.Med.Bff.Infrastructure.Http.ServiceRequestResponse;

namespace Fiap.Health.Med.Bff.Application.Mappers
{
    public static class GetDoctorsByFiltersHandlerMappers
    {
        public static GetDoctorsByFiltersServiceRequest MapToServiceRequest(this GetDoctorsByFiltersHandlerRequest request) =>
            new()
            {
                CurrentPage = request.CurrentPage,
                PageSize = request.PageSize,
                DoctorCrmUf = request.DoctorCrmUf,
                DoctorDoncilNumber = request.DoctorDoncilNumber,
                DoctorName = request.DoctorName,
                DoctorSpecialty = request.DoctorSpecialty
            };
    }
}

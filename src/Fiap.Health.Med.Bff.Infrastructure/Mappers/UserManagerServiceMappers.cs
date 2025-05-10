using Fiap.Health.Med.Bff.Infrastructure.Http.HttpResponse;
using Fiap.Health.Med.Bff.Infrastructure.Http.ServiceRequestResponse;

namespace Fiap.Health.Med.Bff.Infrastructure.Http.Mappers
{
    public static class UserManagerServiceMappers
    {
        public static GetDoctorsByFiltersHttpRequest MapToHttpRequest(this GetDoctorsByFiltersServiceRequest self) =>
            new GetDoctorsByFiltersHttpRequest
            {
                DoctorCrmUf = self.DoctorCrmUf,
                DoctorDoncilNumber = self.DoctorDoncilNumber,
                DoctorName = self.DoctorName,
                DoctorSpecialty = self.DoctorSpecialty,
                CurrentPage = self.CurrentPage,
                PageSize = self.PageSize
            };

        public static UpdatePatientByIdHttpRequest MapToHttpRequest(this UpdatePatientByIdServiceRequest self) =>
            new UpdatePatientByIdHttpRequest
            {
                Document = self.Document,
                Email = self.Email,
                HashedPassword = self.HashedPassword,
                Name = self.Name
            };
    }
}

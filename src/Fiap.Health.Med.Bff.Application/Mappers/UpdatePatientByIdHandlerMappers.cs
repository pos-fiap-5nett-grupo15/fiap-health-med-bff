using Fiap.Health.Med.Bff.Application.Handlers.Patient.UpdatePatientById.Models;
using Fiap.Health.Med.Bff.Infrastructure.Http.ServiceRequestResponse;

namespace Fiap.Health.Med.Bff.Application.Mappers
{
    public static class UpdatePatientByIdHandlerMappers
    {
        public static UpdatePatientByIdServiceRequest MapToServiceRequest(this UpdatePatientByIdHandlerRequest self) =>
            new()
            {
                Name = self.Name,
                Document = self.Document,
                HashedPassword = self.Password, // TODO: Aplicar hash de senha
                Email = self.Email
            };
    }
}

using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.Handlers.Patient.UpdatePatientById.Models;

namespace Fiap.Health.Med.Bff.Application.Handlers.Patient.UpdatePatientById.Interfaces
{
    public interface IUpdatePatientByIdHandler
    {
        Task<Result> HandlerAsync(UpdatePatientByIdHandlerRequest request, CancellationToken ct);
    }
}

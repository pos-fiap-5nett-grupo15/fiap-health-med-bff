using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.Handlers.Patient.DeletePatientById.Models;

namespace Fiap.Health.Med.Bff.Application.Handlers.Patient.DeletePatientById.Interfaces
{
    public interface IDeletePatientByIdHandler
    {
        Task<Result> HandlerAsync(DeletePatientByIdHandlerRequest request, CancellationToken ct);
    }
}

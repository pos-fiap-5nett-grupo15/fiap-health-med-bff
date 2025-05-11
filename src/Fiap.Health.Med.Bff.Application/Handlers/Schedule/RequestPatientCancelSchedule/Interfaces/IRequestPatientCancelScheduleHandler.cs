using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.RequestPatientCancelSchedule.Models;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.RequestPatientCancelSchedule.Interfaces
{
    public interface IRequestPatientCancelScheduleHandler
    {
        Task<Result> HandlerAsync(RequestPatientCancelScheduleHandlerRequest request, CancellationToken ct);
    }
}

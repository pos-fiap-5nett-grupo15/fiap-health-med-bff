using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.AcceptScheduleByDoctor.Models;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.AcceptScheduleByDoctor.Interfaces
{
    public interface IAcceptScheduleByDoctorHandler
    {
        Task<Result> HandlerAsync(AcceptScheduleByDoctorHandlerRequest request, CancellationToken ct);
    }
}

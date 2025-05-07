using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.DeclineScheduleByDoctor.Models;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.DeclineScheduleByDoctor.Interfaces
{
    public interface IDeclineScheduleByDoctorHandler
    {
        Task<Result> HandlerAsync(DeclineScheduleByDoctorHandlerRequest request, CancellationToken ct);
    }
}

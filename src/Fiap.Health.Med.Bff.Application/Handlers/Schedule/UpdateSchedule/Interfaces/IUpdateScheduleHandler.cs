using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.UpdateSchedule.Models;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.UpdateSchedule.Interfaces
{
    public interface IUpdateScheduleHandler
    {
        Task<Result> HandlerAsync(UpdateScheduleHandlerRequest request, CancellationToken ct);
    }
}

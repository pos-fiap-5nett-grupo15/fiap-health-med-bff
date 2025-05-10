using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.GetSchedule.Models;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.GetSchedule.Interfaces
{
    public interface IGetScheduleByIdHandler
    {
        Task<Result<GetScheduleHandlerResponse>> HandlerAsync(long scheduleId, CancellationToken ct);
    }
}

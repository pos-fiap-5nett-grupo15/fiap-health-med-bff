using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.GetSchedule.Models;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.GetSchedule.Interfaces
{
    public interface IGetAllSchedulesHandler
    {
        Task<Result<GetScheduleHandlerResponse>> HandlerAsync(CancellationToken ct);
    }
}

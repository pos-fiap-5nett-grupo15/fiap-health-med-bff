using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.CreateScheduleHandler.Models;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.CreateScheduleHandler.Interfaces
{
    public interface ICreateScheduleHandler
    {
        Task<Result> HandlerAsync(CreateScheduleHandlerRequest request, CancellationToken ct);
    }
}

using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.GetSchedule.Models;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.GetSchedule.Interfaces
{
    public interface IGetScheduleByPatientIdHandler
    {
        Task<Result<GetScheduleHandlerResponse>> HandlerAsync(int patientId, CancellationToken ct);
    }
}

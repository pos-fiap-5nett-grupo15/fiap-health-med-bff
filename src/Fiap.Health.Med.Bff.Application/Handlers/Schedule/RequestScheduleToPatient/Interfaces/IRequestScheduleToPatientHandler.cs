using Fiap.Health.Med.Bff.Application.Common;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.RequestScheduleToPatient.Interfaces
{
    public interface IRequestScheduleToPatientHandler
    {
        Task<Result> HandlerAsync(long scheduleId, int patientId, CancellationToken ct);
    }
}

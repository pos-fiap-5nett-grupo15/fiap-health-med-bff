using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.DTOs.Schedule.AcceptScheduleByDoctor;

namespace Fiap.Health.Med.Bff.Application.Interfaces.Schedule.AcceptScheduleByDoctor
{
    public interface IAcceptScheduleByDoctorHandler
    {
        Task<Result> HandlerAsync(AcceptScheduleByDoctorHandlerRequest request, CancellationToken ct);
    }
}

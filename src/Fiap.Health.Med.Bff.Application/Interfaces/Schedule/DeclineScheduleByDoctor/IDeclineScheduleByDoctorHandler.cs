using Fiap.Health.Med.Bff.Application.Common;
using Fiap.Health.Med.Bff.Application.DTOs.Common;
using Fiap.Health.Med.Bff.Application.DTOs.Schedule.DeclineScheduleByDoctor;

namespace Fiap.Health.Med.Bff.Application.Interfaces.Schedule.DeclineScheduleByDoctor
{
    public interface IDeclineScheduleByDoctorHandler
    {
        Task<Result> HandlerAsync(DeclineScheduleByDoctorHandlerRequest request, CancellationToken ct);
    }
}

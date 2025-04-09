using Fiap.Health.Med.Bff.Application.DTOs.Common;
using Fiap.Health.Med.Bff.Application.DTOs.Schedule;

namespace Fiap.Health.Med.Bff.Application.Interfaces.Schedule
{
    public interface IScheduleHandler
    {
        Task<HandlerResultDto> UpdateScheduleAsync(UpdateScheduleRequestDto requestData);
    }
}

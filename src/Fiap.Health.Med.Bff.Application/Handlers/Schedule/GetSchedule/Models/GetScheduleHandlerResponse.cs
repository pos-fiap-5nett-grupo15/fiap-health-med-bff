using Fiap.Health.Med.Bff.Domain.Dtos;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.GetSchedule.Models
{
    public class GetScheduleHandlerResponse
    {
        public IEnumerable<GetScheduleResponse>? Schedules { get; set; }
    }
}

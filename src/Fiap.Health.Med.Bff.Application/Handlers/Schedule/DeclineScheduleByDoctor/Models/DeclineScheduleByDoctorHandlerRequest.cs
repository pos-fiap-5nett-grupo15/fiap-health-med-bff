namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.DeclineScheduleByDoctor.Models
{
    public class DeclineScheduleByDoctorHandlerRequest
    {
        public long ScheduleId { get; init; }
        public int DoctorId { get; init; }
    }
}

namespace Fiap.Health.Med.Bff.Application.DTOs.Schedule.DeclineScheduleByDoctor
{
    public class DeclineScheduleByDoctorHandlerRequest
    {
        public long ScheduleId { get; init; }
        public int DoctorId { get; init; }
    }
}

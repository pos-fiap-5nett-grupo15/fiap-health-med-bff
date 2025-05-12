namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.AcceptScheduleByDoctor.Models
{
    public class AcceptScheduleByDoctorHandlerRequest
    {
        public long ScheduleId { get; init; }
        public int DoctorId { get; init; }
    }
}

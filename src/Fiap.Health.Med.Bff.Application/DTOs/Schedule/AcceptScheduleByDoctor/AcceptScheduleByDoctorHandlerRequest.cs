namespace Fiap.Health.Med.Bff.Application.DTOs.Schedule.AcceptScheduleByDoctor
{
    public class AcceptScheduleByDoctorHandlerRequest
    {
        public long ScheduleId { get; init; }
        public int DoctorId { get; init; }
    }
}

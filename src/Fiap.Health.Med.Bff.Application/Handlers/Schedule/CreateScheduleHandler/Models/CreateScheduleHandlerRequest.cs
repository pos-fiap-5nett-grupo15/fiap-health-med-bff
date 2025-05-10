namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.CreateScheduleHandler.Models
{
    public class CreateScheduleHandlerRequest
    {
        public int DoctorId { get; set; }
        public DateTime ScheduleTime { get; set; }
        public float Price { get; set; }
    }
}

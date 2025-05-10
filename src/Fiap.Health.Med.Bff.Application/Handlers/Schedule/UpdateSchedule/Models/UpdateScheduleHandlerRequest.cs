using System.Text.Json.Serialization;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.UpdateSchedule.Models
{
    public class UpdateScheduleHandlerRequest
    {
        [JsonIgnore]
        public long ScheduleId { get; set; }
        [JsonIgnore]
        public int DoctorId { get; set; }
        public DateTime ScheduleTime { get; set; }
        public float Price { get; set; }
    }
}

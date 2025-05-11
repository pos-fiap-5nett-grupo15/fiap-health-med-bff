using System.Text.Json.Serialization;

namespace Fiap.Health.Med.Bff.Application.Handlers.Schedule.RequestPatientCancelSchedule.Models
{
    public class RequestPatientCancelScheduleHandlerRequest
    {
        [JsonIgnore]
        public long ScheduleId { get; set; }
        [JsonIgnore]
        public int PatientId { get; set; }
        public required string Reason { get; set; }
    }
}

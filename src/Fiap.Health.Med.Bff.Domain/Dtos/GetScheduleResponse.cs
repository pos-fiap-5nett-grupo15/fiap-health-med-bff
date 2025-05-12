using Fiap.Health.Med.Bff.Domain.Enums;

namespace Fiap.Health.Med.Bff.Domain.Dtos
{
    public class GetScheduleResponse
    {
        public long Id { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public float Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime ScheduleTime { get; set; }
        public EScheduleStatus Status { get; set; }
        public string StatusDescription { get { return Status.ToString(); } }
        public string CancelReason { get; set; } = string.Empty;
    }
}

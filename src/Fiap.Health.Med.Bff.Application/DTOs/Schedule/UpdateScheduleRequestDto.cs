using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fiap.Health.Med.Bff.Application.DTOs.Schedule
{
    public class UpdateScheduleRequestDto
    {
        [JsonIgnore]
        public long Id { get; set; }

        [Required(ErrorMessage = "Data do agendamento é obrigatória")]
        public DateTime? ScheduleTime { get; set; }
    }
}

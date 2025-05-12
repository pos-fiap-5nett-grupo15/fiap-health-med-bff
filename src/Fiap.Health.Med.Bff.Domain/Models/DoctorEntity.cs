using Fiap.Health.Med.Bff.Domain.Enums;

namespace Fiap.Health.Med.Bff.Domain.Models
{
    public class DoctorEntity
    {
        public int Id { get; set; }
        public int CrmNumber { get; set; }
        public string CrmUf { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public EMedicalSpecialty MedicalSpecialty { get; set; }
    }
}

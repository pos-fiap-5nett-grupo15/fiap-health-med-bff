using Fiap.Health.Med.Infra.Enums;
using System.ComponentModel.DataAnnotations;

namespace Fiap.Health.Med.Bff.Application.DTOs.Doctor.Create
{
    public class CreateNewDoctorRequestDto
    {
        [Required(ErrorMessage = "UF do CRM é obrigatório")]
        [Length(2, 2, ErrorMessage = "UF deve conter 2 caracteres")]
        public required string CrmUf { get; set; }

        [Required(ErrorMessage = "Número do CRM é obrigatório")]
        public required int CrmNumber { get; set; }

        [Required(ErrorMessage = "Nome do médico é obrigatório")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Email é obrigatória")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Especialidade do médico é obrigatória")]
        public required EMedicalSpecialty MedicalSpecialty { get; set; }
    }
}

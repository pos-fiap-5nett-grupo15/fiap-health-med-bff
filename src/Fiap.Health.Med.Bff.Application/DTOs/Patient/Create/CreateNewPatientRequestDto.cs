using System.ComponentModel.DataAnnotations;

namespace Fiap.Health.Med.Bff.Application.DTOs.Patient.Create
{
    public class CreateNewPatientRequestDto
    {
        [Required(ErrorMessage = "Nome do paciente é obrigatório")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Documento é obrigatório")]
        public required int Document { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        public required string Email { get; set; }
    }
}

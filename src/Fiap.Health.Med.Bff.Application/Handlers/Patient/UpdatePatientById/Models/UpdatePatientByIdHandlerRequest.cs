namespace Fiap.Health.Med.Bff.Application.Handlers.Patient.UpdatePatientById.Models
{
    public class UpdatePatientByIdHandlerRequest
    {
        public int PatientId { get; init; }
        public string? Name { get; set; }
        public long? Document { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }
}

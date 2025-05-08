namespace Fiap.Health.Med.Bff.Application.Handlers.Patient.UpdatePatientById.Models
{
    public class UpdatePatientByIdRequestBody
    {
        public string? Name { get; init; }
        public long? Document { get; init; }
        public string? Password { get; init; }
        public string? Email { get; init; }
    }
}

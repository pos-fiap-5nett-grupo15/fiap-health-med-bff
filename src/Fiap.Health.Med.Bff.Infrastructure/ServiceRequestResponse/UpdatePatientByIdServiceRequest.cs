namespace Fiap.Health.Med.Bff.Infrastructure.Http.ServiceRequestResponse
{
    public class UpdatePatientByIdServiceRequest
    {
        public string? Name { get; set; }
        public long? Document { get; set; }
        public string? HashedPassword { get; set; }
        public string? Email { get; set; }
    }
}

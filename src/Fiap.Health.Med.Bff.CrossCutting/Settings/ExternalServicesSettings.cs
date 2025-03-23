namespace Fiap.Health.Med.Bff.CrossCutting.Settings
{
    public class ExternalServicesSettings
    {
        public required ServiceSettings DoctorService { get; set; }
        public required ServiceSettings PatientService { get; set; }
    }
    public class ServiceSettings
    {
        public required string BaseURL { get; set; }
        public required IEnumerable<EndpointSettings> Endpoints { get; set; }
    }
    public class EndpointSettings
    {
        public required string Name { get; set; }
        public required string Resource { get; set; }
    }
}

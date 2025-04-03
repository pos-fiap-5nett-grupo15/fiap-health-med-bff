namespace Fiap.Health.Med.Bff.CrossCutting.Settings
{
    public class ExternalServicesSettings
    {
        public required ServiceSettings UserService { get; set; }
        public required ServiceSettings ScheduleService { get; set; }
    }
    public class ServiceSettings
    {
        public required string BaseURL { get; set; }
        public required IEnumerable<EndpointSettings> Endpoints { get; set; }
        public string GetEndpoint(string endpointKey)
        {
            var resource = Endpoints.FirstOrDefault(e => e.Name == endpointKey)?.Resource ?? throw new KeyNotFoundException(endpointKey);
            return $"{BaseURL}/{resource}";
        }
        public string GetResource(string resourceKey) => Endpoints.FirstOrDefault(e => e.Name == resourceKey)?.Resource ?? throw new KeyNotFoundException(resourceKey);
    }
    public class EndpointSettings
    {
        public required string Name { get; set; }
        public required string Resource { get; set; }
    }
}

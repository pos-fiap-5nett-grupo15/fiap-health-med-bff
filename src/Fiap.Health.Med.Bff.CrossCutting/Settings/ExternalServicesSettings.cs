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
    }
}

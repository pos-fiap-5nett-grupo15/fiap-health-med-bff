using Fiap.Health.Med.Bff.Domain.Dtos;

namespace Fiap.Health.Med.Bff.Infrastructure.Http.ServiceRequestResponse
{
    public class GetScheduleServiceResponse : BaseServiceResponse
    {
        public IEnumerable<GetScheduleResponse>? Schedules { get; set; }
    }
}

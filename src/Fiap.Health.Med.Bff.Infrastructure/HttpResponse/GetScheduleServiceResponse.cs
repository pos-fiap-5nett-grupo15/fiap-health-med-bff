using Fiap.Health.Med.Bff.Domain.Dtos;
using Fiap.Health.Med.Bff.Infrastructure.Http.ServiceRequestResponse;

namespace Fiap.Health.Med.Bff.Infrastructure.Http.HttpResponse
{
    public class GetScheduleServiceResponse : BaseServiceResponse
    {
        public IEnumerable<GetScheduleResponse>? Schedules { get; set; }
    }
}

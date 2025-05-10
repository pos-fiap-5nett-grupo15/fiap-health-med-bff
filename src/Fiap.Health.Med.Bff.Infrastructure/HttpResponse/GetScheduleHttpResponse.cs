using Fiap.Health.Med.Bff.Domain.Dtos;

namespace Fiap.Health.Med.Bff.Infrastructure.Http.HttpResponse
{
    public class GetScheduleHttpResponse : BaseHttpResponse
    {
        public IEnumerable<GetScheduleResponse>? Schedules;
    }
}

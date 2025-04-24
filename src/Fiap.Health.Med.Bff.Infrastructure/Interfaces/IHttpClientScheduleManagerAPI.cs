using Fiap.Health.Med.Bff.Infrastructure.Http.HttpResponse;

namespace Fiap.Health.Med.Bff.Infrastructure.Http.Interfaces
{
    public interface IHttpClientScheduleManagerAPI
    {
        Task<DeclineScheduleByIdHttpResponse?> DeclineScheduleByIdAsync(
            string authorization,
            long scheduleId,
            int doctorId,
            CancellationToken ct);
        Task<DeclineScheduleByIdHttpResponse?> AcceptScheduleByIdAsync(
          string authorization,
          long scheduleId,
          int doctorId,
          CancellationToken ct);
    }
}

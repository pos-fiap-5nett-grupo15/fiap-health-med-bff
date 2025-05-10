using Fiap.Health.Med.Bff.Infrastructure.Http.HttpResponse;

namespace Fiap.Health.Med.Bff.Infrastructure.Http.Services
{
    public interface IScheduleManagerService
    {
        Task<BaseServiceResponse> CreateScheduleAsync(int doctorId, DateTime scheduleTime, float price, CancellationToken ct);
        Task<BaseServiceResponse> DeclineScheduleByIdAsync(long scheduleId, int doctorId, CancellationToken ct);
        Task<BaseServiceResponse> AcceptScheduleByIdAsync(long scheduleId, int doctorId, CancellationToken ct);
        Task<BaseServiceResponse> UpdateScheduleByIdAsync(long scheduleId, int doctorId, DateTime scheduleDate, float schedulePrice, CancellationToken ct);
    }
}

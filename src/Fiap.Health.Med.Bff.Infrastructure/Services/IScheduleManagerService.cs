using Fiap.Health.Med.Bff.Infrastructure.Http.ServiceRequestResponse;

namespace Fiap.Health.Med.Bff.Infrastructure.Http.Services
{
    public interface IScheduleManagerService
    {
        Task<GetScheduleServiceResponse> GetAllSchedulesAsync(CancellationToken ct);
        Task<GetScheduleServiceResponse> GetScheduleByIdAsync(long scheduleId, CancellationToken ct);
        Task<GetScheduleServiceResponse> GetSchedulesByDoctorIdAsync(int doctorId, CancellationToken ct);
        Task<GetScheduleServiceResponse> GetSchedulesByPatientIdAsync(int patientId, CancellationToken ct);
        Task<BaseServiceResponse> CreateScheduleAsync(int doctorId, DateTime scheduleTime, float price, CancellationToken ct);
        Task<BaseServiceResponse> DeclineScheduleByIdAsync(long scheduleId, int doctorId, CancellationToken ct);
        Task<BaseServiceResponse> AcceptScheduleByIdAsync(long scheduleId, int doctorId, CancellationToken ct);
        Task<BaseServiceResponse> UpdateScheduleByIdAsync(long scheduleId, int doctorId, DateTime scheduleDate, float schedulePrice, CancellationToken ct);
        Task<BaseServiceResponse> RequestScheduleToPatientAsync(long scheduleId, int patientId, CancellationToken ct);
        Task<BaseServiceResponse> RequestPatientCancelScheduleAsync(long scheduleId, int patientId, string reason, CancellationToken ct);
    }
}

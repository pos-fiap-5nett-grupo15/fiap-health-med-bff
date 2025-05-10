using Fiap.Health.Med.Bff.Infrastructure.Http.HttpResponse;

namespace Fiap.Health.Med.Bff.Infrastructure.Http.Interfaces
{
    public interface IHttpClientScheduleManagerAPI
    {
        Task<GetScheduleHttpResponse> GetScheduleByIdAsync(
            string authorization,
            long scheduleId,
            CancellationToken ct);

        Task<GetScheduleHttpResponse> GetSchedulesByDoctorIdAsync(
            string authorization,
            int doctorId,
            CancellationToken ct);

        Task<GetScheduleHttpResponse> GetSchedulesByPatientIdAsync(
            string authorization,
            int patientId,
            CancellationToken ct);

        Task<CreateScheduleHttpResponse> CreateScheduleAsync(
            string authorization,
            int doctorId,
            DateTime scheduleDate,
            float schedulePrice,
            CancellationToken ct);

        Task<UpdateScheduleHttpResponse?> UpdateScheduleByIdAsync(
            string authorization,
            long scheduleId,
            int doctorId,
            DateTime scheduleDate,
            float schedulePrice,
            CancellationToken ct);
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

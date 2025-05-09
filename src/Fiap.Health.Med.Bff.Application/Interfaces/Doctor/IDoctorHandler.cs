using Fiap.Health.Med.Bff.Application.Dtos.Auth.UserSearch;
using Fiap.Health.Med.Bff.Application.DTOs.Common;
using Fiap.Health.Med.Bff.Application.DTOs.Doctor;

namespace Fiap.Health.Med.Bff.Application.Interfaces.Doctor
{
    public interface IDoctorHandler
    {
        Task<HandlerResultDto> CreateNewDoctorAsync(DoctorRequestDto requestData);
        Task<HandlerResultDto?> PutDoctorAsync(int id, DoctorRequestDto requestData);
        Task<HandlerResultDto?> DeleteDoctorAsync(int id);
        Task<HandlerResultDto?> GetAllDoctorAsync();
        Task<HandlerResultDto?> GetByIdDoctor(int id);
        Task<UserSearchResponseDto?> GetDoctorByConcilAsync(string uf, int crm);
    }
}

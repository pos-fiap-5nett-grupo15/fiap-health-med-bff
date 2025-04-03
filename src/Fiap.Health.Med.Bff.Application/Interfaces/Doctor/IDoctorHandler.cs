using Fiap.Health.Med.Bff.Application.Dtos.Auth.UserSearch;
using Fiap.Health.Med.Bff.Application.DTOs.Doctor.Create;

namespace Fiap.Health.Med.Bff.Application.Interfaces.Doctor
{
    public interface IDoctorHandler
    {
        Task<CreateNewDoctorResponseDto> CreateNewDoctorAsync(CreateNewDoctorRequestDto requestData);
        Task<UserSearchResponseDto?> GetDoctorByConcilAsync(string uf, int crm);
    }
}

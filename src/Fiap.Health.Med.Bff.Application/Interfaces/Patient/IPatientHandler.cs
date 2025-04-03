using Fiap.Health.Med.Bff.Application.Dtos.Auth.UserSearch;
using Fiap.Health.Med.Bff.Application.DTOs.Patient.Create;

namespace Fiap.Health.Med.Bff.Application.Interfaces.Patient
{
    public interface IPatientHandler
    {
        Task<CreateNewPatientResponseDto> CreateNewPatientAsync(CreateNewPatientRequestDto requestData);
        Task<UserSearchResponseDto?> GetPatientByCpfAsync(int cpf);
    }
}

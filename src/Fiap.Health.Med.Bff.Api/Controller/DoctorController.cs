using Fiap.Health.Med.Bff.Application.DTOs.Doctor;
using Fiap.Health.Med.Bff.Application.Handlers.Doctor.GetDoctorsByFilters.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Doctor.GetDoctorsByFilters.Models;
using Fiap.Health.Med.Bff.Application.Interfaces.Doctor;
using Fiap.Health.Med.Bff.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Health.Med.Bff.Api.Controller
{
    [ApiController]
    [Route("[Controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorHandler _doctorHandler;
        private readonly IGetDoctorsByFiltersHandler _getDoctorsByFiltersHandler;

        public DoctorController(
            IDoctorHandler doctorHandler,
            IGetDoctorsByFiltersHandler getDoctorsByFiltersHandler)
        {
            _doctorHandler = doctorHandler;
            _getDoctorsByFiltersHandler = getDoctorsByFiltersHandler;
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdDoctor(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _doctorHandler.GetByIdDoctor(id);

            return StatusCode((int)result.StatusCode, result.ResponseData);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateNewDoctor(DoctorRequestDto requestData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (await _doctorHandler.GetDoctorByConcilAsync(requestData.CrmUf, requestData.CrmNumber) is not null)
                return BadRequest("Doctor already exists");

            var result = await _doctorHandler.CreateNewDoctorAsync(requestData);

            return StatusCode((int)result.StatusCode, result.ResponseData);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = nameof(EUserType.Doctor))]
        public async Task<IActionResult> PutDoctor(int id, DoctorRequestDto requestData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _doctorHandler.GetDoctorByConcilAsync(requestData.CrmUf, requestData.CrmNumber) is null)
                return BadRequest("Doctor does not exist");

            var result = await _doctorHandler.PutDoctorAsync(id, requestData);

            return StatusCode((int)result.StatusCode, result.ResponseData);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = nameof(EUserType.Doctor))]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _doctorHandler.GetByIdDoctor(id) is null)
                return BadRequest("Doctor does not exist");

            var result = await _doctorHandler.DeleteDoctorAsync(id);

            return StatusCode((int)result.StatusCode, result.ResponseData);
        }

        [HttpGet("filters")]
        //[Authorize(Roles = nameof(EUserType.Patient))] TODO: Descomentar antes de entregar a versão final
        public async Task<IActionResult> GetDoctorsByFiltersAsync(
            [FromQuery] string? doctorName,
            [FromQuery] Fiap.Health.Med.Bff.Domain.Enums.EMedicalSpecialty? doctorSpecialty,
            [FromQuery] int? doctorDoncilNumber,
            [FromQuery] string? doctorCrmUf,
            [FromQuery] int? currentPage,
            [FromQuery] int? pageSize,
            CancellationToken ct)
        {
            var request = new GetDoctorsByFiltersHandlerRequest
            {
                DoctorCrmUf = doctorCrmUf,
                DoctorDoncilNumber = doctorDoncilNumber,
                DoctorName = doctorName,
                DoctorSpecialty = doctorSpecialty,
                CurrentPage = currentPage ?? 1,
                PageSize = pageSize ?? 10
            };

            var result = await _getDoctorsByFiltersHandler.HandlerAsync(request, ct);

            return StatusCode((int)result.StatusCode, result.Data);
        }
    }
}

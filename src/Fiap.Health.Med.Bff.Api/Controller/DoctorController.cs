using Fiap.Health.Med.Bff.Application.DTOs.Doctor;
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

        public DoctorController(IDoctorHandler doctorHandler)
        {
            _doctorHandler = doctorHandler;
        }

        [HttpGet("list")]
        [Authorize]
        public async Task<IActionResult> GetAllDoctor()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _doctorHandler.GetAllDoctorAsync();

            return StatusCode((int)result.StatusCode, result.ResponseData);
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
        [Authorize]
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
    }
}

using Fiap.Health.Med.Bff.Application.DTOs.Patient.Create;
using Fiap.Health.Med.Bff.Application.Handlers.Patient.DeletePatientById.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Patient.DeletePatientById.Models;
using Fiap.Health.Med.Bff.Application.Interfaces.Patient;
using Fiap.Health.Med.Infra.Api;
using Fiap.Health.Med.Infra.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Health.Med.Bff.Api.Controller
{
    public class PatientController : BaseController
    {
        private readonly IPatientHandler _patientHandler;
        private readonly IDeletePatientByIdHandler _deletePatientByIdHandler;

        public PatientController(
            IPatientHandler patientHandler,
            IDeletePatientByIdHandler deletePatientByIdHandler)
        {
            _patientHandler = patientHandler;
            _deletePatientByIdHandler = deletePatientByIdHandler;
        }

        [HttpGet]
        [Authorize(Roles = nameof(EUserType.Patient))]
        public IActionResult Test()
        {
            return Ok("Teste");
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewDoctor(CreateNewPatientRequestDto requestData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (await _patientHandler.GetPatientByCpfAsync(requestData.Document) is not null)
                return BadRequest("Doctor already exists");

            var result = await _patientHandler.CreateNewPatientAsync(requestData);

            return StatusCode((int)result.StatusCode, result.ResponseData);
        }

        [HttpDelete("{patientId}")]
        //[Authorize(Roles = nameof(EUserType.Patient))] TODO: Descomentar antes de entregar a versão final
        public async Task<IActionResult> DeletePatientByIdAsync(
            [FromRoute] int patientId,
            CancellationToken ct)
        {
            var request = new DeletePatientByIdHandlerRequest
            {
                DoctorId = patientId
            };

            var result = await _deletePatientByIdHandler.HandlerAsync(request, ct);

            if (result.IsSuccess)
                return StatusCode((int)result.StatusCode);

            return StatusCode((int)result.StatusCode, result);
        }
    }
}

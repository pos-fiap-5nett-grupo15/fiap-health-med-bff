using Fiap.Health.Med.Bff.Application.Handlers.Schedule.AcceptScheduleByDoctor.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.AcceptScheduleByDoctor.Models;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.CreateScheduleHandler.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.CreateScheduleHandler.Models;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.DeclineScheduleByDoctor.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.DeclineScheduleByDoctor.Models;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.GetSchedule.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.UpdateSchedule.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.UpdateSchedule.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Health.Med.Bff.Api.Controller
{
    [ApiController]
    [Route("[Controller]")]
    public class ScheduleController : ControllerBase
    {
        private readonly IGetScheduleByIdHandler _getScheduleByIdHandler;
        private readonly IGetScheduleByDoctorIdHandler _getScheduleByDoctorIdHandler;
        private readonly IGetScheduleByPatientIdHandler _getScheduleByPatientIdHandler;
        private readonly ICreateScheduleHandler _createScheduleHandler;
        private readonly IUpdateScheduleHandler _updateScheduleHandler;
        private readonly IDeclineScheduleByDoctorHandler _declineScheduleByDoctorHandler;
        private readonly IAcceptScheduleByDoctorHandler _acceptScheduleByDoctorHandler;

        public ScheduleController(
            IGetScheduleByIdHandler getScheduleByIdHandler,
            IGetScheduleByDoctorIdHandler getScheduleByDoctorIdHandler,
            IGetScheduleByPatientIdHandler getScheduleByPatientIdHandler,
            ICreateScheduleHandler createScheduleHandler,
            IUpdateScheduleHandler updateScheduleHandler,
            IDeclineScheduleByDoctorHandler declineScheduleByDoctorHandler,
            IAcceptScheduleByDoctorHandler acceptScheduleByDoctorHandler)
        {
            _getScheduleByIdHandler = getScheduleByIdHandler;
            _getScheduleByDoctorIdHandler = getScheduleByDoctorIdHandler;
            _getScheduleByPatientIdHandler = getScheduleByPatientIdHandler;
            _createScheduleHandler = createScheduleHandler;
            _updateScheduleHandler = updateScheduleHandler;
            _declineScheduleByDoctorHandler = declineScheduleByDoctorHandler;
            _acceptScheduleByDoctorHandler = acceptScheduleByDoctorHandler;
        }

        [HttpGet("{scheduleId}")]
        public async Task<IActionResult> GetById([FromRoute] long scheduleId, CancellationToken ct)
        {
            var result = await _getScheduleByIdHandler.HandlerAsync(scheduleId, ct);

            if (result.IsSuccess)
            {
                if (result.Data is null || result.Data.Schedules is null || result.Data.Schedules.Count() <= 0)
                    return NotFound("Agendamento não encontrado");
                else
                    return StatusCode((int)result.StatusCode, result.Data.Schedules.FirstOrDefault());
            }

            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctorId([FromRoute] int doctorId, CancellationToken ct)
        {
            var result = await _getScheduleByDoctorIdHandler.HandlerAsync(doctorId, ct);

            if (result.IsSuccess)
            {
                if (result.Data is null || result.Data.Schedules is null || result.Data.Schedules.Count() <= 0)
                    return NotFound("Nenhum agendamento não encontrado");
                else
                    return StatusCode((int)result.StatusCode, result.Data.Schedules);
            }

            return StatusCode((int)result.StatusCode, result);
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatientId([FromRoute] int patientId, CancellationToken ct)
        {
            var result = await _getScheduleByPatientIdHandler.HandlerAsync(patientId, ct);

            if (result.IsSuccess)
            {
                if (result.Data is null || result.Data.Schedules is null || result.Data.Schedules.Count() <= 0)
                    return NotFound("Nenhum agendamento não encontrado");
                else
                    return StatusCode((int)result.StatusCode, result.Data.Schedules);
            }

            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule([FromBody] CreateScheduleHandlerRequest requestData, CancellationToken ct)
        {
            var result = await _createScheduleHandler.HandlerAsync(requestData, ct);

            if (result.IsSuccess)
                return StatusCode((int)result.StatusCode);

            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPut("{scheduleId}/doctor/{doctorId}/update")]
        [Authorize]
        public async Task<IActionResult> UpdateSchedule(long scheduleId, int doctorId, [FromBody] UpdateScheduleHandlerRequest requestData, CancellationToken ct)
        {
            requestData.ScheduleId = scheduleId;
            requestData.DoctorId = doctorId;

            var result = await _updateScheduleHandler.HandlerAsync(requestData, ct);

            if (result.IsSuccess)
                return StatusCode((int)result.StatusCode);

            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPatch("{scheduleId}/doctor/{doctorId}/decline")]
        public async Task<IActionResult> DeclineScheduleAsync(
            [FromRoute] long scheduleId,
            [FromRoute] int doctorId,
            CancellationToken ct)
        {
            var request = new DeclineScheduleByDoctorHandlerRequest
            {
                ScheduleId = scheduleId,
                DoctorId = doctorId,
            };

            var result = await _declineScheduleByDoctorHandler.HandlerAsync(request, ct);

            if (result.IsSuccess)
                return StatusCode((int)result.StatusCode);

            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPatch("{scheduleId}/doctor/{doctorId}/accept")]
        [Authorize]
        public async Task<IActionResult> AcceptScheduleAsync(
            [FromRoute] long scheduleId,
            [FromRoute] int doctorId,
            CancellationToken ct)
        {
            var request = new AcceptScheduleByDoctorHandlerRequest
            {
                ScheduleId = scheduleId,
                DoctorId = doctorId,
            };

            var result = await _acceptScheduleByDoctorHandler.HandlerAsync(request, ct);

            if (result.IsSuccess)
                return StatusCode((int)result.StatusCode);

            return StatusCode((int)result.StatusCode, result);
        }
    }
}

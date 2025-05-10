using Fiap.Health.Med.Bff.Application.Handlers.Schedule.AcceptScheduleByDoctor.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.AcceptScheduleByDoctor.Models;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.DeclineScheduleByDoctor.Interfaces;
using Fiap.Health.Med.Bff.Application.Handlers.Schedule.DeclineScheduleByDoctor.Models;
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
        private readonly IDeclineScheduleByDoctorHandler _declineScheduleByDoctorHandler;
        private readonly IAcceptScheduleByDoctorHandler _acceptScheduleByDoctorHandler;
        private readonly IUpdateScheduleHandler _updateScheduleHandler;

        public ScheduleController(
            IDeclineScheduleByDoctorHandler declineScheduleByDoctorHandler,
            IAcceptScheduleByDoctorHandler acceptScheduleByDoctorHandler,
            IUpdateScheduleHandler updateScheduleHandler)
        {
            _declineScheduleByDoctorHandler = declineScheduleByDoctorHandler;
            _acceptScheduleByDoctorHandler = acceptScheduleByDoctorHandler;
            _updateScheduleHandler = updateScheduleHandler;
        }



        [HttpPut("{scheduleId}/{doctorId}")]
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

        [HttpPatch("{scheduleId}/decline/{doctorId}")]
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

        [HttpPatch("{scheduleId}/accept/{doctorId}")]
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

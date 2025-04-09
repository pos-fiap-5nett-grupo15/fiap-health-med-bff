using Fiap.Health.Med.Bff.Application.DTOs.Schedule;
using Fiap.Health.Med.Bff.Application.Interfaces.Schedule;
using Fiap.Health.Med.Infra.Api;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Health.Med.Bff.Api.Controller
{
    public class ScheduleController : BaseController
    {
        private readonly IScheduleHandler _scheduleHandler;

        public ScheduleController(IScheduleHandler scheduleHandler)
        {
            _scheduleHandler = scheduleHandler;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSchedule(int id, [FromBody] UpdateScheduleRequestDto requestData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            else
                requestData.Id = id;

            var result = await _scheduleHandler.UpdateScheduleAsync(requestData);

            object? responseData;

            if (result.ResponseData is not null)
                responseData = result.ResponseData;
            else if (!string.IsNullOrEmpty(result.ErrorMessage))
                responseData = result.ErrorMessage;
            else
                responseData = null;


            return StatusCode((int)result.StatusCode, responseData);
        }
    }
}

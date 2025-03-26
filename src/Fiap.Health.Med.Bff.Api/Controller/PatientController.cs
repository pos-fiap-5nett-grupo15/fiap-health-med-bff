using Fiap.Health.Med.Infra.Api;
using Fiap.Health.Med.Infra.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Health.Med.Bff.Api.Controller
{
    [Authorize(Roles = nameof(EUserType.Patient))]
    public class PatientController : BaseController
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok("Patient Test");
        }
    }
}

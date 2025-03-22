using Fiap.Health.Med.Bff.Application.DTOs.Auth;
using Fiap.Health.Med.Infra.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Health.Med.Bff.Api.Controller
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> AuthenticateUser(LoginRequestDTO requestData)
        {
            return NotFound();
        }
    }
}

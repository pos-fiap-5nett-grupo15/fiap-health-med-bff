using Fiap.Health.Med.Bff.Application.DTOs.Auth.Authenticate;
using Fiap.Health.Med.Bff.Application.Interfaces.Auth;
using Fiap.Health.Med.Infra.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.Health.Med.Bff.Api.Controller
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly IAuthenticationHandler _authenticationHandler;

        public AuthController(IAuthenticationHandler authenticationHandler)
        {
            _authenticationHandler = authenticationHandler;
        }

        [HttpPost]
        public async Task<IActionResult> AuthenticateUser(LoginRequestDto requestData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authenticationHandler.AuthenticateAsync(requestData);
            return string.IsNullOrEmpty(result.AccessToken) ? Unauthorized(result.Message) : Ok(result);
        }
    }
}

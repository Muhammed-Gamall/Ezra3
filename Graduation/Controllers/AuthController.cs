using Graduation.Contracts.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Graduation.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private readonly IAuthService _authService = authService;

        [HttpPost]
        public async Task<IActionResult> AuthenticationAsync(Contracts.Authentication.LoginRequest Request, CancellationToken cancellationToken)
        {
            var result = await _authService.LoginAsync(Request, cancellationToken);

            return result is null ? BadRequest("Email or Password are Wrong") : Ok(result);
        }


        [HttpPost("Refresh")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshTokenRequest Request)
        {
            var result = await _authService.GetRefreshTokenAsync(Request.Token, Request.RefreshToken);

            return result is null ? BadRequest("Invalid token") : Ok(result);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(SignUpRequest Request, string Role, CancellationToken cancellationToken)
        {
            var result = await _authService.SignUpAsync(Request, Role, cancellationToken);

            return result is null ? BadRequest("Registration failed") : Ok(result);
        }
    }
}

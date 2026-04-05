using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Graduation.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UserProfilesController(IUserService userService) : ControllerBase
    {
        private readonly IUserService userService = userService;

        [HttpGet]
        public async Task<IActionResult> GetUserAsync(CancellationToken cancellation = default)
        {
            var user = await userService.GetUserAsync(cancellation);

            return user is null? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(UserRequest request, CancellationToken cancellation = default)
        {
            var user = await userService.CreateUserAsync(request, cancellation);

            return  Ok(user);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync(UserRequest request, CancellationToken cancellation = default)
        {
            var user = await userService.UpdateUserAsync(request, cancellation);

            return  Ok(user);
        }

        [HttpPost("Toggle")]
        public async Task<IActionResult> ToggleAsync( CancellationToken cancellation = default)
        {
            await userService.ToggleAsync(cancellation);
            return  Ok();
        }

    }
}

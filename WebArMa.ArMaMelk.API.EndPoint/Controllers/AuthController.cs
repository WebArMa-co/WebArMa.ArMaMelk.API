using Asp.Versioning;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using WebArMa.ArMaMelk.API.Application.Auth.Commands.Login;
using WebArMa.ArMaMelk.API.Application.Auth.Commands.Logout;

namespace WebArMa.ArMaMelk.API.EndPoint.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var data = await mediator.Send(command);
            return Ok(data);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Logout(LogoutCommand command)
        {
            var data = await mediator.Send(command);
            return Ok(data);
        }
    }
}

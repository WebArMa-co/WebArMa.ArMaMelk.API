using Asp.Versioning;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using WebArMa.ArMaMelk.API.Application.UserPersons.Commands.UpdateUserPerson;

namespace WebArMa.ArMaMelk.API.EndPoint.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserPersonController(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserPersonCommand command)
        {
            var data = await mediator.Send(command);
            return Ok(data);
        }
    }
}

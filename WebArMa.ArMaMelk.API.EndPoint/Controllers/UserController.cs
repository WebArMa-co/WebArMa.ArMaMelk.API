using Asp.Versioning;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using WebArMa.ArMaMelk.API.Application.Users.Commands.Update;
using WebArMa.ArMaMelk.API.Application.Users.Queries.GetByGuid;

namespace WebArMa.ArMaMelk.API.EndPoint.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserController(IMediator mediator) : ControllerBase
    {
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCommand command)
        {
            var data = await mediator.Send(command);
            return Ok(data);
        }

        [HttpGet]
        [Route("{Guid:guid}")]
        public async Task<IActionResult> GetByGuid([FromRoute] GetByGuidQuery query)
        {
            var data = await mediator.Send(query);
            return Ok(data);
        }
    }
}

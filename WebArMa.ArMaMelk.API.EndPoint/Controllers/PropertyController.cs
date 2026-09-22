using Asp.Versioning;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using WebArMa.ArMaMelk.API.Application.Properties.Commands.Create;
using WebArMa.ArMaMelk.API.Application.Properties.Commands.Delete;
using WebArMa.ArMaMelk.API.Application.Properties.Commands.Update;

namespace WebArMa.ArMaMelk.API.EndPoint.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PropertyController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateCommand command)
        {
            var data = await mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCommand command)
        {
            var data = await mediator.Send(command);
            return Ok(data);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteCommand command)
        {
            var data = await mediator.Send(command);
            return Ok(data);
        }
    }
}

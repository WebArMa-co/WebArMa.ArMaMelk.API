using Asp.Versioning;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using WebArMa.ArMaMelk.API.Application.UserPersons.Commands.Create;
using WebArMa.ArMaMelk.API.Application.UserPersons.Commands.Delete;
using WebArMa.ArMaMelk.API.Application.UserPersons.Commands.Update;
using WebArMa.ArMaMelk.API.Application.UserPersons.Queries.Get;
using WebArMa.ArMaMelk.API.Application.UserPersons.Queries.GetByGuid;

namespace WebArMa.ArMaMelk.API.EndPoint.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserPersonController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Get(CreateCommand command)
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
        [Route("{Guid:guid}")]
        public async Task<IActionResult> Delete(DeleteCommand command)
        {
            var data = await mediator.Send(command);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Get(GetQuery query)
        {
            var data = await mediator.Send(query);
            return Ok(data);
        }

        [HttpGet]
        [Route("{Guid:guid}")]
        public async Task<IActionResult> Get(GetByGuidQuery query)
        {
            var data = await mediator.Send(query);
            return Ok(data);
        }
    }
}

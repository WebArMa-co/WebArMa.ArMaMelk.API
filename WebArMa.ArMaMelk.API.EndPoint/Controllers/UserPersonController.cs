using Asp.Versioning;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using WebArMa.ArMaMelk.API.Application.UserPersons.Commands.CreateUserPerson;
using WebArMa.ArMaMelk.API.Application.UserPersons.Commands.UpdateUserPerson;
using WebArMa.ArMaMelk.API.Application.UserPersons.Queries.GetUserPersons;

namespace WebArMa.ArMaMelk.API.EndPoint.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UserPersonController(IMediator mediator) : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Get(CreateUserPersonCommand command)
        {
            var data = await mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserPersonCommand command)
        {
            var data = await mediator.Send(command);
            return Ok(data);
        }

        [HttpGet]
        public async Task<IActionResult> Get(GetUserPersonsQuery query)
        {
            var data = await mediator.Send(query);
            return Ok(data);
        }
    }
}

using Asp.Versioning;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using WebArMa.ArMaMelk.API.Application.Persons.Commands.CreatePerson;
using WebArMa.ArMaMelk.API.Application.Persons.Commands.UpdatePerson;
using WebArMa.ArMaMelk.API.Application.Persons.Queries.GetByGuid;

namespace WebArMa.ArMaMelk.API.EndPoint.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PersonController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreatePersonCommand command)
        {
            var data = await mediator.Send(command);
            return Ok(data);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePersonCommand command)
        {
            var data = await mediator.Send(command);
            return Ok(data);
        }

        [HttpGet]
        [Route("{query:guid}")]
        public async Task<IActionResult> GetByGuid(GetByGuidQuery query)
        {
            var data = await mediator.Send(query);
            return Ok(data);
        }
    }
}

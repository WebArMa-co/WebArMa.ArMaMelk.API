using Asp.Versioning;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using WebArMa.ArMaMelk.API.Application.Persons.Queries.GetPersonByGuid;

namespace WebArMa.ArMaMelk.API.EndPoint.Controllers
{
    [ApiController]
    [ApiVersion(1.0)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class OTPController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Route("{query}")]
        public new async Task<IActionResult> Request(GetByGuidQuery query)
        {
            var data = await mediator.Send(query);
            return Ok(data);
        }
    }
}

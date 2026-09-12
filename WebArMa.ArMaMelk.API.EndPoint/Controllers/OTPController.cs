using Asp.Versioning;
using Mediator;
using Microsoft.AspNetCore.Mvc;
using WebArMa.ArMaMelk.API.Application.OTPs.Commands.Request;

namespace WebArMa.ArMaMelk.API.EndPoint.Controllers
{
	[ApiController]
	[ApiVersion(1.0)]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class OTPController(IMediator mediator) : ControllerBase
	{
		[HttpPost]
		[Route("Request")]
		public async Task<IActionResult> SendOtp(RequestCommand command)
		{
			await mediator.Send(command);
			return Ok();
		}
	}
}
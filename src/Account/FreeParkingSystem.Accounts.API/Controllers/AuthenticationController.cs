using System.Threading.Tasks;
using FreeParkingSystem.Accounts.API.Models;
using FreeParkingSystem.Accounts.Contract.Queries;
using FreeParkingSystem.Common.API.ExtensionMethods;
using FreeParkingSystem.Common.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeParkingSystem.Accounts.API.Controllers
{
	[Route("api/auth")]
	[ApiController]
	[AllowAnonymous]
	public class AuthenticationController : ControllerBase
	{
		private readonly IMediator _mediator;

		public AuthenticationController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> Post(UserLoginInput input)
		{
			var result = await _mediator.Send(new UserLoginRequest(input.UserName, input.Password));

			return result.ToActionResult<UserToken>();
		}
	}
}
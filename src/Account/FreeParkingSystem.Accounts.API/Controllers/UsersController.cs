using System.Threading.Tasks;
using FreeParkingSystem.Accounts.API.Models;
using FreeParkingSystem.Accounts.Contract.Commands;
using FreeParkingSystem.Common.API.ExtensionMethods;
using FreeParkingSystem.Common.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeParkingSystem.Accounts.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class UsersController : ControllerBase
	{
		private readonly IMediator _mediator;

		public UsersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> Post(CreateUserInput input)
		{
			var result = await _mediator.Send(new CreateUserRequest(input.UserName, input.Password, input.Email, Role.Member));

			return result.ToActionResult();
		}
	}
}
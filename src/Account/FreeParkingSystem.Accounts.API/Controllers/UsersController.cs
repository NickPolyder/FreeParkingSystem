using System.Threading.Tasks;
using FreeParkingSystem.Accounts.Contract.Commands;
using FreeParkingSystem.Accounts.Contract.Dtos;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.API.ExtensionMethods;
using FreeParkingSystem.Common.ExtensionMethods;
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
		private readonly IMap<CreateUserInput, CreateUserRequest> _createUserMapper;

		public UsersController(IMediator mediator, IMap<CreateUserInput, CreateUserRequest> createUserMapper)
		{
			_mediator = mediator;
			_createUserMapper = createUserMapper;
		}

		[HttpPost]
		public async Task<IActionResult> Post(CreateUserInput input)
		{
			var result = await _mediator.Send(_createUserMapper.Map(input));

			return result.ToActionResult();
		}
	}
}
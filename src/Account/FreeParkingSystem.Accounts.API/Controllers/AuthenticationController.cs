using System.Threading.Tasks;
using FreeParkingSystem.Accounts.Contract.Dtos;
using FreeParkingSystem.Accounts.Contract.Queries;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.API.Controllers;
using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.ExtensionMethods;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeParkingSystem.Accounts.API.Controllers
{
	[Route("api/auth")]
	[ApiController]
	[AllowAnonymous]
	public class AuthenticationController : BaseController
	{
		private readonly IMediator _mediator;
		private readonly IMap<UserLoginInput, UserLoginRequest> _userLoginMapper;

		public AuthenticationController(IMediator mediator, IMap<UserLoginInput, UserLoginRequest> userLoginMapper)
		{
			_mediator = mediator;
			_userLoginMapper = userLoginMapper;
		}

		[HttpPost]
		public async Task<IActionResult> Post(UserLoginInput input)
		{
			var result = await _mediator.Send(_userLoginMapper.Map(input));

			return ActionResult<UserToken>(result);
		}
	}
}
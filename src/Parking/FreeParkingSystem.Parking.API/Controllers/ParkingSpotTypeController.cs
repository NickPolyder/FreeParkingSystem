using System.Collections.Generic;
using System.Threading.Tasks;
using FreeParkingSystem.Common.API.Controllers;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreeParkingSystem.Parking.API.Controllers
{
	[Route("api/parking-spot-types")]
	[ApiController]
	public class ParkingSpotTypeController : BaseController
	{
		private readonly IMediator _mediator;

		public ParkingSpotTypeController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var result = await _mediator.Send(new GetParkingSpotTypesRequest());

			return ActionResult<List<ParkingSpotType>>(result);
		}
	}
}
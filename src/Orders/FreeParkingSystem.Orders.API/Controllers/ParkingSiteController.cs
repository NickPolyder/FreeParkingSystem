using System.Collections.Generic;
using System.Threading.Tasks;
using FreeParkingSystem.Common.API.Controllers;
using FreeParkingSystem.Orders.Contract;
using FreeParkingSystem.Orders.Contract.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreeParkingSystem.Orders.API.Controllers
{
	[Route("api/parking-site")]
	[ApiController]
	public class ParkingSiteController : BaseController
	{
		private readonly IMediator _mediator;

		public ParkingSiteController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("{parkingSiteId}/orders")]
		public async Task<IActionResult> Get(int parkingSiteId)
		{
			var result = await _mediator.Send(new GetActiveLeasesByParkingRequest(parkingSiteId));

			return ActionResult<List<OrderView>>(result);
		}
	}
}
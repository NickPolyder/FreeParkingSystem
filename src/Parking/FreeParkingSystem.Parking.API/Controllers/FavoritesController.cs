﻿using System.Threading.Tasks;
using FreeParkingSystem.Common.API.Controllers;
using FreeParkingSystem.Parking.Contract.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreeParkingSystem.Parking.API.Controllers
{
	[ApiController]
	public class FavoritesController : BaseController
	{
		private readonly IMediator _mediator;

		public FavoritesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("api/parking-site/{id}/favorite")]
		public async Task<IActionResult> PostParkingSite(int id)
		{
			var result = await _mediator.Send(new AddParkingSiteToFavoritesRequest(id));

			return ActionResult(result);
		}

		[HttpDelete("api/parking-site/{id}/favorite")]
		public async Task<IActionResult> DeleteParkingSite(int id)
		{
			var result = await _mediator.Send(new DeleteParkingSiteFromFavoritesRequest(id));

			return ActionResult(result);
		}

		[HttpPost("api/parking-site/{parkingSiteId}/parking-spots/{id}/favorite")]
		public async Task<IActionResult> PostParkingSpot(int id)
		{
			var result = await _mediator.Send(new AddParkingSpotToFavoritesRequest(id));

			return ActionResult(result);
		}

		[HttpDelete("api/parking-site/{parkingSiteId}/parking-spots/{id}/favorite")]
		public async Task<IActionResult> DeleteParkingSpot(int id)
		{
			var result = await _mediator.Send(new DeleteParkingSpotFromFavoritesRequest(id));

			return ActionResult(result);
		}
	}
}
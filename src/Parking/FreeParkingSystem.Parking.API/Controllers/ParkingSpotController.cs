using System.Collections.Generic;
using System.Threading.Tasks;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.API.Controllers;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Commands;
using FreeParkingSystem.Parking.Contract.Dtos;
using FreeParkingSystem.Parking.Contract.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FreeParkingSystem.Parking.API.Controllers
{
	[ApiController]
	public class ParkingSpotController : BaseController
	{
		private readonly IMediator _mediator;
		private readonly IMap<AddParkingSpotInput, AddParkingSpotRequest> _addParkingSpotMapper;
		private readonly IMap<ChangeParkingSpotInput, ChangeParkingSpotRequest> _changeParkingSpotMapper;

		public ParkingSpotController(IMediator mediator, 
			IMap<AddParkingSpotInput, AddParkingSpotRequest> addParkingSpotMapper, 
			IMap<ChangeParkingSpotInput, ChangeParkingSpotRequest> changeParkingSpotMapper)
		{
			_mediator = mediator;
			_addParkingSpotMapper = addParkingSpotMapper;
			_changeParkingSpotMapper = changeParkingSpotMapper;
		}

		[HttpGet("api/parking-site/{parkingSiteId}/parking-spots")]
		public async Task<IActionResult> Get(int parkingSiteId)
		{
			var result = await _mediator.Send(new GetParkingSpotsRequest(parkingSiteId));

			return ActionResult<List<ParkingSpot>>(result);
		}

		[HttpGet("api/parking-site/{parkingSiteId}/parking-spots/{id}")]
		public async Task<IActionResult> Get(int parkingSiteId, int id)
		{
			var result = await _mediator.Send(new GetParkingSpotByIdRequest(id, parkingSiteId));

			return ActionResult<ParkingSpot>(result);
		}

		[HttpPost("api/parking-site/{parkingSiteId}/parking-spots")]
		public async Task<IActionResult> Post(int parkingSiteId, AddParkingSpotInput input)
		{
			input.ParkingSiteId = parkingSiteId;
			var result = await _mediator.Send(_addParkingSpotMapper.Map(input));

			return ActionResult<ParkingSpot>(result);
		}


		[HttpPatch("api/parking-site/{parkingSiteId}/parking-spots/{id}")]
		public async Task<IActionResult> Patch(int id, int parkingSiteId, ChangeParkingSpotInput input)
		{
			input.Id = id;
			input.ParkingSiteId = parkingSiteId;

			var result = await _mediator.Send(_changeParkingSpotMapper.Map(input));

			return ActionResult(result);
		}

		[HttpDelete("api/parking-site/{parkingSiteId}/parking-spots/{id}")]
		public async Task<IActionResult> Delete(int id, int parkingSiteId)
		{
			var result = await _mediator.Send(new DeleteParkingSpotRequest(id, parkingSiteId));

			return ActionResult(result);
		}
	}
}
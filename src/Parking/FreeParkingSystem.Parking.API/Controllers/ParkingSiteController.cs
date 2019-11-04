using System.Collections.Generic;
using System.Threading.Tasks;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.API.Controllers;
using FreeParkingSystem.Common.API.ExtensionMethods;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Commands;
using FreeParkingSystem.Parking.Contract.Dtos;
using FreeParkingSystem.Parking.Contract.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeParkingSystem.Parking.API.Controllers
{
	[Route("api/parking-site")]
	[ApiController]
	[Authorize]
	public class ParkingSiteController : BaseController
	{
		private readonly IMediator _mediator;
		private readonly IMap<AddParkingSiteInput, AddParkingSiteRequest> _parkingSiteInputMapper;

		private readonly IMap<ChangeParkingSiteDetailsInput, ChangeParkingSiteDetailsRequest>
			_parkingSiteChangeDetailsMapper;

		public ParkingSiteController(IMediator mediator,
			IMap<AddParkingSiteInput, AddParkingSiteRequest> parkingSiteInputMapper,
			IMap<ChangeParkingSiteDetailsInput, ChangeParkingSiteDetailsRequest> parkingSiteChangeDetailsMapper)
		{
			_mediator = mediator;
			_parkingSiteInputMapper = parkingSiteInputMapper;
			_parkingSiteChangeDetailsMapper = parkingSiteChangeDetailsMapper;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var result = await _mediator.Send(new GetParkingSitesRequest());

			return ActionResult<List<ParkingSiteView>>(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var result = await _mediator.Send(new GetParkingSiteByIdRequest(id));

			return ActionResult<ParkingSiteView>(result);
		}

		[HttpPost]
		public async Task<IActionResult> Post(AddParkingSiteInput input)
		{
			var result = await _mediator.Send(_parkingSiteInputMapper.Map(input));

			return ActionResult<ParkingSite>(result);
		}


		[HttpPatch("{id}")]
		public async Task<IActionResult> Patch(int id, ChangeParkingSiteDetailsInput input)
		{
			input.ParkingSiteId = id;

			var result = await _mediator.Send(_parkingSiteChangeDetailsMapper.Map(input));

			return ActionResult(result);
		}
	}
}
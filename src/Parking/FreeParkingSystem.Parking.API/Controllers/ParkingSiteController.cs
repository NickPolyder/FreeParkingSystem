using System.Threading.Tasks;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.API.ExtensionMethods;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Parking.Contract.Commands;
using FreeParkingSystem.Parking.Contract.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FreeParkingSystem.Parking.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class ParkingSiteController : ControllerBase
	{
		private readonly IMediator _mediator;
		private readonly IMap<AddParkingSiteInput, AddParkingSiteRequest> _parkingSiteInputMapper;

		public ParkingSiteController(IMediator mediator, IMap<AddParkingSiteInput, AddParkingSiteRequest> parkingSiteInputMapper)
		{
			_mediator = mediator;
			_parkingSiteInputMapper = parkingSiteInputMapper;
		}

		[HttpPost]
		public async Task<IActionResult> Post(AddParkingSiteInput input)
		{
			var result = await _mediator.Send(_parkingSiteInputMapper.Map(input));

			return result.ToActionResult();
		}
	}
}
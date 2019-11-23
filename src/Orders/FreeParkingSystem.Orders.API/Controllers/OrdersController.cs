using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.API.Controllers;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Orders.Contract;
using FreeParkingSystem.Orders.Contract.Commands;
using FreeParkingSystem.Orders.Contract.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeParkingSystem.Orders.API.Controllers
{
	[Route("api/orders")]
	[ApiController]
	[Authorize]
	public class OrdersController : BaseController
	{
		private readonly IMediator _mediator;
		private readonly IMap<StartLeaseInput, StartLeaseCommand> _startLeaseMapper;

		public OrdersController(IMediator mediator, IMap<StartLeaseInput, StartLeaseCommand> startLeaseMapper)
		{
			_mediator = mediator;
			_startLeaseMapper = startLeaseMapper;
		}

		[HttpPost]
		public async Task<IActionResult> Post(StartLeaseInput input)
		{
			var result = await _mediator.Send(_startLeaseMapper.Map(input));

			return ActionResult<OrderView>(result);
		}

		[HttpDelete("{orderId}")]
		public async Task<IActionResult> Cancel(int orderId)
		{
			var result = await _mediator.Send(new CancelLeaseCommand(orderId));

			return ActionResult<OrderView>(result);
		}
	}
}
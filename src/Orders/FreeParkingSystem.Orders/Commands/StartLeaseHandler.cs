﻿using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.MessageBroker.Contract;
using FreeParkingSystem.Common.Messages;
using FreeParkingSystem.Orders.Contract;
using FreeParkingSystem.Orders.Contract.Commands;
using FreeParkingSystem.Parking.Contract.Messages;
using MediatR;

namespace FreeParkingSystem.Orders.Commands
{
	public class StartLeaseHandler : IRequestHandler<StartLeaseCommand, BaseResponse>
	{
		private readonly IOrderServices _orderServices;
		private readonly IPublishBroker _publishBroker;
		private readonly IUserContextAccessor _userContextAccessor;

		public StartLeaseHandler(IOrderServices orderServices,
			IPublishBroker publishBroker,
			IUserContextAccessor userContextAccessor)
		{
			_orderServices = orderServices;
			_publishBroker = publishBroker;
			_userContextAccessor = userContextAccessor;
		}
		public Task<BaseResponse> Handle(StartLeaseCommand request, CancellationToken cancellationToken)
		{
			var userId = _userContextAccessor.GetUserContext().GetUserId();

			var order = _orderServices.StartLease(request.ParkingSpotId, userId);

			_publishBroker.Publish(new StartLeaseOnParkingSpotMessage(request.ParkingSpotId));

			var orderView = _orderServices.GetView(order.Id, userId);
			orderView.ParkingSpot.IsAvailable = false;

			return request.ToSuccessResponse(orderView).AsTask();
		}
	}
}
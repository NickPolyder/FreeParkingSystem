using System;
using FreeParkingSystem.Common.Data;
using FreeParkingSystem.Orders.Contract;
using FreeParkingSystem.Orders.Contract.Exceptions;
using FreeParkingSystem.Orders.Contract.Repositories;

namespace FreeParkingSystem.Orders
{
	public class OrderServices : IOrderServices
	{
		private readonly ICommonFunctionsRepository _commonFunctionsRepository;
		private readonly IOrderRepository _orderRepository;
		private readonly IOrderViewRepository _orderViewRepository;

		public OrderServices(ICommonFunctionsRepository commonFunctionsRepository,
			IOrderRepository orderRepository,
			IOrderViewRepository orderViewRepository)
		{
			_commonFunctionsRepository = commonFunctionsRepository;
			_orderRepository = orderRepository;
			_orderViewRepository = orderViewRepository;
		}

		public OrderView GetView(int orderId)
		{
			return _orderViewRepository.Get(orderId);
		}

		public Order StartLease(int parkingSpotId, int userId)
		{
			if (userId < 0)
				throw new OrderException(Contract.Resources.Validations.Order_UserIdNotValid);

			if (parkingSpotId < 1 || !_commonFunctionsRepository.ParkingSpotExists(parkingSpotId))
				throw new OrderException(Contract.Resources.Validations.ParkingSpot_DoesNotExist);

			if(_commonFunctionsRepository.HasActiveLeaseOnAParkingSpot(parkingSpotId))
				throw new OrderException(Contract.Resources.Validations.ParkingSpot_IsAlreadyLeased);

			return _orderRepository.Add(new Order
			{
				ParkingSpotId = parkingSpotId,
				UserId = userId,
				LeaseStartDate = DateTime.UtcNow
			});
		}

		public Order EndLease(int orderId)
		{
			if (orderId < 1)
				throw new OrderException();

			var order = _orderRepository.Get(orderId);

			if (order == null)
				throw new OrderException(nameof(order));

			order.LeaseEndDate = DateTime.UtcNow;

			return _orderRepository.Update(order);
		}
	}
}
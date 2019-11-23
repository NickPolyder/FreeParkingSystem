using System;
using System.Collections.Generic;
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

		public IEnumerable<OrderView> GetViews(int userId)
		{
			return _orderViewRepository.GetActiveLeases(userId);
		}

		public OrderView GetView(int orderId, int userId)
		{
			var order = _orderViewRepository.Get(orderId);

			if (order != null 
			    && (order.TenantId != userId 
					&& order.ParkingSpot.ParkingSite.OwnerId != userId))
			{
				order = null;
			}

			return order;
		}

		public IEnumerable<OrderView> GetActiveLeasesByParking(int parkingSiteId, int userId)
		{
			return _orderViewRepository.GetActiveLeasesByParking(parkingSiteId, userId);
		}

		public Order StartLease(int parkingSpotId, int userId)
		{
			if (userId < 0)
				throw new OrderException(Contract.Resources.Validations.Order_UserIdNotValid);

			if (parkingSpotId < 1 || !_commonFunctionsRepository.ParkingSpotExists(parkingSpotId))
				throw new OrderException(Contract.Resources.Validations.ParkingSpot_DoesNotExist);

			if (_commonFunctionsRepository.HasActiveLeaseOnAParkingSpot(parkingSpotId))
				throw new OrderException(Contract.Resources.Validations.ParkingSpot_IsAlreadyLeased);

			if (_commonFunctionsRepository.IsOwnerOfParkingSpot(parkingSpotId, userId))
				throw new OrderException(Contract.Resources.Validations.ParkingSpot_OwnerCannotLeaseToHimself);

			return _orderRepository.Add(new Order
			{
				ParkingSpotId = parkingSpotId,
				TenantId = userId,
				LeaseStartDate = DateTime.UtcNow
			});
		}

		public Order CancelLease(int orderId, int userId)
		{
			var order = _orderRepository.Get(orderId);

			if (order == null)
				throw new OrderException(Contract.Resources.Validations.Order_DoesNotExist);

			if (userId < 0)
				throw new OrderException(Contract.Resources.Validations.Order_UserIdNotValid);

			if (order.IsCancelled)
				throw new OrderException(Contract.Resources.Validations.Order_AlreadyCancelled);

			if (order.LeaseEndDate.HasValue)
				throw new OrderException(Contract.Resources.Validations.Order_AlreadyEnded);

			if (IsOwnerOrTenant(order, userId))
				throw new OrderException(Contract.Resources.Validations.Order_CanOnlyBeCancelledByTheOwnerOrTheTennant);

			order.IsCancelled = true;
			order.LeaseEndDate = DateTime.UtcNow;

			_orderRepository.Update(order);

			return order;
		}

		private bool IsOwnerOrTenant(Order order, int userId)
		{
			return order.TenantId != userId
				   && !_commonFunctionsRepository.IsOwnerOfParkingSpot(order.ParkingSpotId, userId);
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
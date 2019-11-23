using System.Collections.Generic;

namespace FreeParkingSystem.Orders.Contract
{
	public interface IOrderServices
	{
		IEnumerable<OrderView> GetViews(int userId);
		IEnumerable<OrderView> GetActiveLeasesByParking(int parkingSiteId, int userId);
		OrderView GetView(int orderId,int userId);
		Order StartLease(int parkingSpotId, int userId);
		Order CancelLease(int orderId, int userId);

		Order EndLease(int orderId);

	}
}
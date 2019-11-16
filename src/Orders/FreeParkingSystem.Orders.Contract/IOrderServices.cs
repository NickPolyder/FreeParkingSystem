﻿namespace FreeParkingSystem.Orders.Contract
{
	public interface IOrderServices
	{
		OrderView GetView(int orderId);
		Order StartLease(int parkingSpotId, int userId);

		Order EndLease(int orderId);

	}
}
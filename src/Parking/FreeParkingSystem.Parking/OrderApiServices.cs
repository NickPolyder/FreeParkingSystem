using System;
using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Parking.Contract;

namespace FreeParkingSystem.Parking
{
	public class OrderApiServices: IOrderApiServices 
	{
		public Task<bool> ParkingSiteHasActiveOrders(int parkingSiteId, CancellationToken cts = default)
		{
			// TODO: Implement it with an actual call to the orders api.
			return ((new Random().Next(parkingSiteId - 1, parkingSiteId + 1)) == parkingSiteId).AsTask();
		}

		public Task<bool> ParkingSpotHasActiveOrders(int parkingSpotId, CancellationToken cts = default)
		{
			// TODO: Implement it with an actual call to the orders api.
			return ((new Random().Next(parkingSpotId - 1, parkingSpotId + 1)) == parkingSpotId).AsTask();
		}
	}
}
using System;
using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Parking.Contract;

namespace FreeParkingSystem.Parking
{
	public class OrderApiServices: IOrderApiServices 
	{
		public Task<bool> HasActiveOrder(int parkingSiteId, CancellationToken cts = default)
		{
			// TODO: Implement it with an actual call to the orders api.
			return ((new Random().Next(parkingSiteId - 1, parkingSiteId + 1)) == parkingSiteId).AsTask();
		}
	}
}
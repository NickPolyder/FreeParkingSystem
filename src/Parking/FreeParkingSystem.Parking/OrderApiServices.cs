using System;
using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Common.Data;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Parking.Contract;

namespace FreeParkingSystem.Parking
{
	public class OrderApiServices: IOrderApiServices 
	{
		private readonly ICommonFunctionsRepository _commonFunctionsRepository;

		public OrderApiServices(ICommonFunctionsRepository commonFunctionsRepository)
		{
			_commonFunctionsRepository = commonFunctionsRepository;
		}
		public Task<bool> ParkingSiteHasActiveOrders(int parkingSiteId, CancellationToken cts = default)
		{
			return _commonFunctionsRepository.HasActiveLeaseOnAParkingSite(parkingSiteId).AsTask();
		}

		public Task<bool> ParkingSpotHasActiveOrders(int parkingSpotId, CancellationToken cts = default)
		{
			return _commonFunctionsRepository.HasActiveLeaseOnAParkingSpot(parkingSpotId).AsTask();
		}
	}
}
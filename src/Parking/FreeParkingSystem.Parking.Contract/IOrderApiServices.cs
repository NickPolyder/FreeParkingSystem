using System.Threading;
using System.Threading.Tasks;

namespace FreeParkingSystem.Parking.Contract
{
	public interface IOrderApiServices
	{
		Task<bool> ParkingSiteHasActiveOrders(int parkingSiteId, CancellationToken cts = default);

		Task<bool> ParkingSpotHasActiveOrders(int parkingSpotId, CancellationToken cts = default);
	}
}
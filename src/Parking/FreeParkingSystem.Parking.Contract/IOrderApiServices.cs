using System.Threading;
using System.Threading.Tasks;

namespace FreeParkingSystem.Parking.Contract
{
	public interface IOrderApiServices
	{
		Task<bool> HasActiveOrder(int parkingSiteId, CancellationToken cts = default);
	}
}
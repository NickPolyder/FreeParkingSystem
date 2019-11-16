using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.Messages;

namespace FreeParkingSystem.Orders.Contract.Commands
{
	[AuthorizeRequest]
	public class StartLeaseCommand: BaseRequest
	{
		public int ParkingSpotId { get; }
		public StartLeaseCommand(int parkingSpotId)
		{
			ParkingSpotId = parkingSpotId;
		}
	}
}
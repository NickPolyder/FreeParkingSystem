using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.Messages;

namespace FreeParkingSystem.Orders.Contract.Queries
{
	[AuthorizeRequest]
	public class GetActiveLeasesByParkingRequest : BaseRequest
	{
		public int ParkingSiteId { get; }

		public GetActiveLeasesByParkingRequest(int parkingSiteId)
		{
			ParkingSiteId = parkingSiteId;
		}
	}
}
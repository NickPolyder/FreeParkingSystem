using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.Messages;

namespace FreeParkingSystem.Parking.Contract.Queries
{
	[AuthorizeRequest]
	public class GetParkingSpotsRequest : BaseRequest
	{
		public int ParkingSiteId { get; }

		public GetParkingSpotsRequest(int parkingSiteId)
		{
			ParkingSiteId = parkingSiteId;
		}
	}
}
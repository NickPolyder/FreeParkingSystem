using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.Messages;

namespace FreeParkingSystem.Parking.Contract.Queries
{
	[AuthorizeRequest]
	public class GetParkingSpotByIdRequest : BaseRequest
	{
		public int Id { get; }
		public int ParkingSiteId { get; }

		public GetParkingSpotByIdRequest(int id,int parkingSiteId)
		{
			Id = id;
			ParkingSiteId = parkingSiteId;
		}
	}
}
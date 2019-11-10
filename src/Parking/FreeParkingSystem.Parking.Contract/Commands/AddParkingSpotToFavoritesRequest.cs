using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.Messages;

namespace FreeParkingSystem.Parking.Contract.Commands
{
	[AuthorizeRequest]
	public class AddParkingSpotToFavoritesRequest : BaseRequest
	{
		public int ParkingSpotId { get; }
		public AddParkingSpotToFavoritesRequest(int parkingSpotId)
		{
			ParkingSpotId = parkingSpotId;
		}
	}
}
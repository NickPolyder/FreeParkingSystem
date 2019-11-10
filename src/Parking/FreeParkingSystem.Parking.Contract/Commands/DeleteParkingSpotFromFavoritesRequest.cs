using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.Messages;

namespace FreeParkingSystem.Parking.Contract.Commands
{
	[AuthorizeRequest]
	public class DeleteParkingSpotFromFavoritesRequest : BaseRequest
	{
		public int ParkingSpotId { get; }
		public DeleteParkingSpotFromFavoritesRequest(int parkingSpotId)
		{
			ParkingSpotId = parkingSpotId;
		}
	}
}
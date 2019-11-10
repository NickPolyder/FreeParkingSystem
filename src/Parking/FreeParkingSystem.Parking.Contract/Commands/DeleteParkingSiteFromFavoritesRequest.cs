using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.Messages;

namespace FreeParkingSystem.Parking.Contract.Commands
{
	[AuthorizeRequest]
	public class DeleteParkingSiteFromFavoritesRequest : BaseRequest
	{
		public int ParkingSiteId { get; }
		public DeleteParkingSiteFromFavoritesRequest(int parkingSiteId)
		{
			ParkingSiteId = parkingSiteId;
		}
	}
}
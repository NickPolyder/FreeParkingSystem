using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.Messages;

namespace FreeParkingSystem.Parking.Contract.Commands
{
	[AuthorizeRequest(Role.Member)]
	public class AddParkingSiteToFavoritesRequest : BaseRequest
	{
		public int ParkingSiteId { get; }
		public AddParkingSiteToFavoritesRequest(int parkingSiteId)
		{
			ParkingSiteId = parkingSiteId;
		}
	}
}
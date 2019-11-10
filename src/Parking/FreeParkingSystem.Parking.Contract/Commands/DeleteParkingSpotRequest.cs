using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.Messages;

namespace FreeParkingSystem.Parking.Contract.Commands
{
	[AuthorizeRequest(Role.Owner)]
	public class DeleteParkingSpotRequest : BaseRequest
	{
		public int Id { get; }
		public int ParkingSiteId { get; }

		public DeleteParkingSpotRequest(int id, int parkingSiteId)
		{
			Id = id;
			ParkingSiteId = parkingSiteId;
		}

	}
}
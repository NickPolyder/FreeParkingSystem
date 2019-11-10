using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.Messages;

namespace FreeParkingSystem.Parking.Contract.Commands
{
	[AuthorizeRequest(Role.Owner)]
	public class AddParkingSpotRequest: BaseRequest
	{
		public int ParkingSiteId { get; }
		public int ParkingSpotTypeId { get; }
		public int Level { get; }
		public int Position { get; }

		public AddParkingSpotRequest(int parkingSiteId, int parkingSpotTypeId, int level, int position)
		{
			ParkingSiteId = parkingSiteId;
			ParkingSpotTypeId = parkingSpotTypeId;
			Level = level;
			Position = position;
		}
	}
}
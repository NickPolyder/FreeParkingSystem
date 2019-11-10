using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.Messages;

namespace FreeParkingSystem.Parking.Contract.Commands
{
	[AuthorizeRequest(Role.Owner)]
	public class ChangeParkingSpotRequest : BaseRequest
	{
		public int Id { get; }
		public int ParkingSiteId { get; } 
		public int? ParkingSpotTypeId { get; }
		public int? Level { get; }
		public int? Position { get; }
		public bool? IsAvailable { get; }

		public ChangeParkingSpotRequest(int id, int parkingSiteId, int? parkingSpotTypeId, int? level, int? position, bool? isAvailable)
		{
			Id = id;
			ParkingSiteId = parkingSiteId;
			ParkingSpotTypeId = parkingSpotTypeId;
			Level = level;
			Position = position;
			IsAvailable = isAvailable;
		}
	}
}
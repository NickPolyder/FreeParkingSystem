using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.Messages;

namespace FreeParkingSystem.Parking.Contract.Commands
{
	[AuthorizeRequest(Role.Owner)]
	public class ChangeParkingSiteDetailsRequest: BaseRequest
	{

		public int ParkingSiteId { get;  }

		public string Name { get; }

		public int ParkingTypeId { get; }

		public bool IsActive { get; }

		public Geolocation Geolocation { get; }

		public ChangeParkingSiteDetailsRequest(int parkingSiteId, string name, int parkingTypeId, bool isActive, Geolocation geolocation)
		{
			ParkingSiteId = parkingSiteId;
			Name = name;
			ParkingTypeId = parkingTypeId;
			IsActive = isActive;
			Geolocation = geolocation;
		}

	}
}
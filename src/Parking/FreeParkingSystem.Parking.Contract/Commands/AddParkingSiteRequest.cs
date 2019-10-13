using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.Messages;

namespace FreeParkingSystem.Parking.Contract.Commands
{
	[AuthorizeRequest(Role.Member)]
	public class AddParkingSiteRequest : BaseRequest
	{
		public string Name { get; }

		public int ParkingTypeId { get; }

		public Geolocation Geolocation { get; }

		public AddParkingSiteRequest(string name, int parkingTypeId, Geolocation geolocation)
		{
			Name = name;
			ParkingTypeId = parkingTypeId;
			Geolocation = geolocation;
		}
	}
}
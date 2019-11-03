using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.Messages;

namespace FreeParkingSystem.Parking.Contract.Commands
{
	[AuthorizeRequest(Role.Owner)]
	public class ChangeParkingSiteDetailsRequest: BaseRequest
	{
		public int ParkingSiteId { get; set; }

		public string Name { get; set; }

		public int ParkingTypeId { get; set; }

		public bool IsActive { get; set; }

		public Geolocation Geolocation { get; set; }

	}
}
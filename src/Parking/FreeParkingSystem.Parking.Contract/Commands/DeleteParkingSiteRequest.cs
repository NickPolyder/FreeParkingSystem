using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.Messages;

namespace FreeParkingSystem.Parking.Contract.Commands
{
	[AuthorizeRequest(Role.Owner)]
	public class DeleteParkingSiteRequest : BaseRequest
	{
		public int Id { get; }

		public DeleteParkingSiteRequest(int id)
		{
			Id = id;
		}

	}
}
using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.Messages;

namespace FreeParkingSystem.Parking.Contract.Queries
{
	[AuthorizeRequest]
	public class GetParkingTypesRequest : BaseRequest
	{

	}
}
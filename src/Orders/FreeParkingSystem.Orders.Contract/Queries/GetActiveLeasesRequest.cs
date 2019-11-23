using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.Messages;

namespace FreeParkingSystem.Orders.Contract.Queries
{
	[AuthorizeRequest]
	public class GetActiveLeasesRequest : BaseRequest
	{
		public GetActiveLeasesRequest()
		{
		}
	}
}
using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.Messages;

namespace FreeParkingSystem.Orders.Contract.Queries
{
	[AuthorizeRequest]
	public class GetActiveLeaseByIdRequest : BaseRequest
	{
		public int OrderId { get; }

		public GetActiveLeaseByIdRequest(int orderId)
		{
			OrderId = orderId;
		}
	}
}
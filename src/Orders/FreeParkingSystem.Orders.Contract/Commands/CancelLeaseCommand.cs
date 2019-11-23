using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.Messages;

namespace FreeParkingSystem.Orders.Contract.Commands
{
	[AuthorizeRequest]
	public class CancelLeaseCommand : BaseRequest
	{
		public int OrderId { get; }
		public CancelLeaseCommand(int orderId)
		{
			OrderId = orderId;
		}
	}
}
using FreeParkingSystem.Common.MessageBroker.Contract;

namespace FreeParkingSystem.Accounts.Contract.Messages
{
	public class UserDeletedParkingSiteMessage : BaseMessageBrokerRequest
	{
		public int UserId { get; }

		public UserDeletedParkingSiteMessage(int userId)
		{
			UserId = userId;
		}
	}
}
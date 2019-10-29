using FreeParkingSystem.Common.MessageBroker.Contract;

namespace FreeParkingSystem.Accounts.Contract.Messages
{
	public class UserCreatedParkingSiteMessage : BaseMessageBrokerRequest
	{
		public int UserId { get; }

		public UserCreatedParkingSiteMessage(int userId)
		{
			UserId = userId;
		}


	}
}
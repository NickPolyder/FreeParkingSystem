using System.Threading.Tasks;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Contract.Messages;
using FreeParkingSystem.Common.MessageBroker.Contract;

namespace FreeParkingSystem.Accounts.Messages
{
	public class UserCreatedParkingSiteHandler : IMessageBrokerHandler<UserCreatedParkingSiteMessage>
	{
		private readonly IUserServices _userServices;

		public UserCreatedParkingSiteHandler(IUserServices userServices)
		{
			_userServices = userServices;
		}

		public void Handle(UserCreatedParkingSiteMessage message)
		{
			
		}
	}
}
using FreeParkingSystem.Accounts.Contract.Messages;
using FreeParkingSystem.Accounts.Messages;
using FreeParkingSystem.Common.MessageBroker.Contract;

namespace FreeParkingSystem.Accounts.Seeder
{
	public class SubscriptionStarter:Autofac.IStartable
	{
		private readonly ISubscriptionBroker _subscriptionBroker;

		public SubscriptionStarter(ISubscriptionBroker subscriptionBroker)
		{
			_subscriptionBroker = subscriptionBroker;
		}
		public void Start()
		{
			_subscriptionBroker.Subscribe<UserCreatedParkingSiteMessage, UserCreatedParkingSiteHandler>();
		}
	}
}
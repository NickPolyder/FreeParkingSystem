using FreeParkingSystem.Common.MessageBroker.Contract;
using FreeParkingSystem.Parking.Contract.Messages;
using FreeParkingSystem.Parking.Messages;

namespace FreeParkingSystem.Parking.Seeder
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
			_subscriptionBroker.Subscribe<StartLeaseOnParkingSpotMessage, StartLeaseOnParkingSpotHandler>();
			_subscriptionBroker.Subscribe<EndLeaseOnParkingSpotMessage, EndLeaseOnParkingSpotHandler>();
		}
	}
}
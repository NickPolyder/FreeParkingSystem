using FreeParkingSystem.Common.MessageBroker.Contract;

namespace FreeParkingSystem.Parking.Contract.Messages
{
	public class StartLeaseOnParkingSpotMessage: BaseMessageBrokerRequest
	{
		public int ParkingSpotId { get; }

		public StartLeaseOnParkingSpotMessage(int parkingSpotId)
		{
			ParkingSpotId = parkingSpotId;
		}
	}
}
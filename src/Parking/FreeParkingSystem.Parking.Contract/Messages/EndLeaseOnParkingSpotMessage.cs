using FreeParkingSystem.Common.MessageBroker.Contract;

namespace FreeParkingSystem.Parking.Contract.Messages
{
	public class EndLeaseOnParkingSpotMessage : BaseMessageBrokerRequest
	{
		public int ParkingSpotId { get; }

		public EndLeaseOnParkingSpotMessage(int parkingSpotId)
		{
			ParkingSpotId = parkingSpotId;
		}
	}
}
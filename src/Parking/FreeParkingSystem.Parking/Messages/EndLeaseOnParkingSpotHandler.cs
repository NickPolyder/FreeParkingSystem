using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.MessageBroker.Contract;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Messages;
using Microsoft.Extensions.Logging;

namespace FreeParkingSystem.Parking.Messages
{
	public class EndLeaseOnParkingSpotHandler : BaseMessageBrokerHandler<EndLeaseOnParkingSpotMessage>
	{
		private readonly IParkingSpotServices _parkingSpotServices;

		public EndLeaseOnParkingSpotHandler(ILoggerFactory loggerFactory, IParkingSpotServices parkingSpotServices) : base(loggerFactory)
		{
			_parkingSpotServices = parkingSpotServices;
		}

		public override void Process(EndLeaseOnParkingSpotMessage message)
		{
			var parkingSpot = _parkingSpotServices.Get(message.ParkingSpotId);
			if (parkingSpot == null)
			{
				Logger.LogWarning(Contract.Resources.Messages.ParkingSpot_DoesNotExist.WithArgs(message.ParkingSpotId));
				return;
			}

			if (parkingSpot.IsAvailable)
			{
				Logger.LogError(Contract.Resources.Messages.ParkingSpot_IsAlreadyAvailable.WithArgs(message.ParkingSpotId));
				return;
			}

			parkingSpot.IsAvailable = true;

			_parkingSpotServices.Update(parkingSpot);

			Logger.LogInformation(Contract.Resources.Messages.ParkingSpot_AvailabilityChanged.WithArgs(parkingSpot.Id));
		}
	}
}
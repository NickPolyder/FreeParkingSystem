using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.MessageBroker.Contract;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Messages;
using Microsoft.Extensions.Logging;

namespace FreeParkingSystem.Parking.Messages
{
	public class StartLeaseOnParkingSpotHandler : BaseMessageBrokerHandler<StartLeaseOnParkingSpotMessage>
	{
		private readonly IParkingSpotServices _parkingSpotServices;

		public StartLeaseOnParkingSpotHandler(ILoggerFactory loggerFactory,IParkingSpotServices parkingSpotServices) : base(loggerFactory)
		{
			_parkingSpotServices = parkingSpotServices;
		}

		public override void Process(StartLeaseOnParkingSpotMessage message)
		{
			var parkingSpot = _parkingSpotServices.Get(message.ParkingSpotId);
			if (parkingSpot == null)
			{
				Logger.LogWarning(Contract.Resources.Messages.ParkingSpot_DoesNotExist.WithArgs(message.ParkingSpotId));
				return;
			}

			if (!parkingSpot.IsAvailable)
			{
				Logger.LogError(Contract.Resources.Messages.ParkingSpot_IsNotAvailable.WithArgs(message.ParkingSpotId));
				return;
			}

			parkingSpot.IsAvailable = false;

			_parkingSpotServices.Update(parkingSpot);

			Logger.LogInformation(Contract.Resources.Messages.ParkingSpot_AvailabilityChanged.WithArgs(parkingSpot.Id));
		}
	}
}
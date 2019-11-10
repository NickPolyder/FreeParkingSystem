
using System.Collections.Generic;
using FreeParkingSystem.Common;
using FreeParkingSystem.Parking.Contract.Commands;
using FreeParkingSystem.Parking.Contract.Dtos;

namespace FreeParkingSystem.Parking.Mappers
{
	public class ChangeParkingSpotInputMapper : IMap<ChangeParkingSpotInput, ChangeParkingSpotRequest>
	{
		public ChangeParkingSpotRequest Map(ChangeParkingSpotInput input, IDictionary<object, object> context)
		{
			return new ChangeParkingSpotRequest(input.Id, input.ParkingSiteId, input.ParkingSpotTypeId, input.Level, input.Position, input.IsAvailable);
		}

		public ChangeParkingSpotInput Map(ChangeParkingSpotRequest input, IDictionary<object, object> context)
		{
			return new ChangeParkingSpotInput
			{
				Id = input.Id,
				ParkingSiteId = input.ParkingSiteId,
				ParkingSpotTypeId = input.ParkingSpotTypeId,
				Level = input.Level,
				Position = input.Position,
				IsAvailable = input.IsAvailable
			};
		}
	}
}
using System.Collections.Generic;
using FreeParkingSystem.Common;
using FreeParkingSystem.Parking.Contract.Commands;
using FreeParkingSystem.Parking.Contract.Dtos;

namespace FreeParkingSystem.Parking.Mappers
{
	public class AddParkingSpotInputMapper : IMap<AddParkingSpotInput, AddParkingSpotRequest>
	{
		public AddParkingSpotRequest Map(AddParkingSpotInput input, IDictionary<object, object> context)
		{
			return new AddParkingSpotRequest(input.ParkingSiteId, input.ParkingSpotTypeId, input.Level, input.Position);
		}

		public AddParkingSpotInput Map(AddParkingSpotRequest input, IDictionary<object, object> context)
		{
			return new AddParkingSpotInput
			{
				ParkingSiteId = input.ParkingSiteId,
				ParkingSpotTypeId = input.ParkingSpotTypeId,
				Level = input.Level,
				Position = input.Position,
			};
		}
	}
}
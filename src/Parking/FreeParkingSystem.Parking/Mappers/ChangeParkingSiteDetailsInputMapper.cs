using System.Collections.Generic;
using FreeParkingSystem.Common;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Commands;
using FreeParkingSystem.Parking.Contract.Dtos;

namespace FreeParkingSystem.Parking.Mappers
{
	public class ChangeParkingSiteDetailsInputMapper : IMap<ChangeParkingSiteDetailsInput, ChangeParkingSiteDetailsRequest>
	{
		public ChangeParkingSiteDetailsRequest Map(ChangeParkingSiteDetailsInput input, IDictionary<object, object> context)
		{
			return new ChangeParkingSiteDetailsRequest(input.ParkingSiteId, input.Name, input.ParkingTypeId, input.IsActive, input.Geolocation);
		}

		public ChangeParkingSiteDetailsInput Map(ChangeParkingSiteDetailsRequest input, IDictionary<object, object> context)
		{
			return new ChangeParkingSiteDetailsInput
			{
				ParkingSiteId = input.ParkingSiteId,
				ParkingTypeId = input.ParkingTypeId,
				Name = input.Name,
				IsActive = input.IsActive,
				Geolocation = input.Geolocation
			};
		}
	}
}
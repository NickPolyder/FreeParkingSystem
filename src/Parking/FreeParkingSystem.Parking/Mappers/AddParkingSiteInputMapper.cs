using System.Collections.Generic;
using FreeParkingSystem.Common;
using FreeParkingSystem.Parking.Contract.Commands;
using FreeParkingSystem.Parking.Contract.Dtos;

namespace FreeParkingSystem.Parking.Mappers
{
	public class AddParkingSiteInputMapper: IMap<AddParkingSiteInput,AddParkingSiteRequest>
	{
		public AddParkingSiteRequest Map(AddParkingSiteInput input, IDictionary<object, object> context)
		{
			return new AddParkingSiteRequest(input.Name, input.ParkingTypeId,input.Geolocation);
		}

		public AddParkingSiteInput Map(AddParkingSiteRequest input, IDictionary<object, object> context)
		{
			return new AddParkingSiteInput
			{
				Name = input.Name,
				ParkingTypeId = input.ParkingTypeId,
				Geolocation = input.Geolocation
			};
		}
	}
}
using System;
using System.Collections.Generic;
using FreeParkingSystem.Common;
using FreeParkingSystem.Orders.Contract.Commands;
using FreeParkingSystem.Orders.Contract.Dtos;

namespace FreeParkingSystem.Orders.Mappers
{
	public class StartLeaseInputMapper: IMap<StartLeaseInput, StartLeaseCommand>
	{
		public StartLeaseCommand Map(StartLeaseInput input, IDictionary<object, object> context)
		{
			if(input == null)
				throw new ArgumentNullException(nameof(input));

			return new StartLeaseCommand(input.ParkingSpotId);
		}

		public StartLeaseInput Map(StartLeaseCommand input, IDictionary<object, object> context)
		{
			if (input == null)
				throw new ArgumentNullException(nameof(input));

			return new StartLeaseInput
			{
				ParkingSpotId = input.ParkingSpotId
			};
		}
	}
}
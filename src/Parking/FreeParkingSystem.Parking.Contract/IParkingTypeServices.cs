﻿using System.Collections.Generic;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Parking.Contract
{
	public interface IParkingTypeServices
	{
		IEnumerable<ParkingType> GetAll();
	}
}
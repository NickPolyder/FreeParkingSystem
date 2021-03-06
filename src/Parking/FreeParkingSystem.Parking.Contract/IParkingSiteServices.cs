﻿using System.Collections.Generic;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Parking.Contract
{
	public interface IParkingSiteServices
	{
		IEnumerable<ParkingSiteView> GetViews();
		ParkingSiteView GetView(int parkingSiteId);
		ParkingSite Get(int parkingSiteId);

		ParkingSite Add(ParkingSite parking);

		ParkingSite Update(ParkingSite parking);

		void Delete(int parkingSiteId);
	}
}
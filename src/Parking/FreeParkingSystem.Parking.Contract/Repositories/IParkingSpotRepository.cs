﻿using System.Collections.Generic;
using FreeParkingSystem.Common;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Parking.Contract.Repositories
{
	public interface IParkingSpotRepository: IRepository<ParkingSpot>
	{
		IEnumerable<ParkingSpot> GetAllBy(int parkingSiteId);
		ParkingSpot GetBy(int parkingSpotId, int parkingSiteId);
		bool Exists(ParkingSpot parkingSpot);

		void DeleteBySiteId(int parkingSiteId);
	}
}
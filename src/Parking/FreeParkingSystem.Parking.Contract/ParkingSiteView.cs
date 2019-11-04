﻿namespace FreeParkingSystem.Parking.Contract
{
	public class ParkingSiteView
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public ParkingType ParkingType { get; set; }
		public bool IsActive { get; set; }
		public string Owner { get; set; }
		public Geolocation Geolocation { get; set; }
	}
}
﻿using System;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Orders.Contract
{
	public class Order
	{
		public int Id { get; set; }

		public int ParkingSpotId { get; set; }

		public ParkingSpot ParkingSpot { get; set; }

		public int UserId { get; set; }

		public DateTime LeaseStartDate { get; set; }

		public DateTime? LeaseEndDate { get; set; }
	}
}
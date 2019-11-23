using System;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Orders.Contract
{
	public class OrderView
	{
		public int Id { get; set; }
		public ParkingSpotView ParkingSpot { get; set; }
		public string Tenant { get; set; }
		public DateTime LeaseStartDate { get; set; }
		public DateTime? LeaseEndDate { get; set; }
		public bool IsCancelled { get; set; }
	}
}
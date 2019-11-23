using System;
using FreeParkingSystem.Parking.Data.Models;
using Newtonsoft.Json;

namespace FreeParkingSystem.Orders.Contract
{
	public class OrderView
	{
		public int Id { get; set; }
		public ParkingSpotView ParkingSpot { get; set; }

		[JsonIgnore]
		public int TenantId { get; set; }
		public string Tenant { get; set; }
		public DateTime LeaseStartDate { get; set; }
		public DateTime? LeaseEndDate { get; set; }
		public bool IsCancelled { get; set; }
	}
}
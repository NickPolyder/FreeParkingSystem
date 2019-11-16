using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FreeParkingSystem.Common.Data.Models;
using FreeParkingSystem.Parking.Data.DatabaseModels;

namespace FreeParkingSystem.Orders.Data.Models
{
	public class DbOrder: IEntity
	{
		[Key]
		public int Id { get; set; }

		public int ParkingSpotId { get; set; }

		public int UserId { get; set; }

		public DateTime LeaseStartDate { get; set; }

		public DateTime? LeaseEndDate { get; set; }

		[ForeignKey(nameof(ParkingSpotId))]
		public virtual DbParkingSpot ParkingSpot { get; set; }
	}
}
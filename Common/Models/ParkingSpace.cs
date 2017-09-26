using System;
using System.ComponentModel.DataAnnotations;

namespace FreeParkingSystem.Common.Models
{
    public class ParkingSpace : BaseModel, IParkingSpace
    {
        public string Description { get; set; }

        [Required]
        public IParkingType ParkingType { get; set; }

        [Required]
        public Position Position { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int Floors { get; set; }

        [Required]
        [Range(1, Int32.MaxValue)]
        public int ParkSpaces { get; set; }

        public bool HasSpecialParkSpaces { get; set; }

        public double? MaxHeightOfVehicle { get; set; }

        public double? WidthOfParkSpot { get; set; }

    }
}

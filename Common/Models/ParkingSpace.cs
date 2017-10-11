using FreeParkingSystem.Common.Services.Validation.Attributes;
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
        [IsPositive]
        public int Floors { get; set; }

        [Required]
        [IsPositive]
        public int ParkSpaces { get; set; }

        public bool HasSpecialParkSpaces { get; set; }

        [IsPositive]
        public double? MaxHeightOfVehicle { get; set; }

        [IsPositive]
        public double? WidthOfParkSpot { get; set; }

    }
}

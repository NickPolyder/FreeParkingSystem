namespace FreeParkingSystem.Common.Models
{
    public class ParkingSpace : BaseModel, IParkingSpace
    {
        public string Description { get; set; }

        public IParkingType ParkingType { get; set; }

        public Position Position { get; set; }

        public int Floors { get; set; }

        public int ParkSpaces { get; set; }

        public bool HasSpecialParkSpaces { get; set; }

        public double? MaxHeightOfVehicle { get; set; }

        public double? WidthOfParkSpot { get; set; }

    }
}

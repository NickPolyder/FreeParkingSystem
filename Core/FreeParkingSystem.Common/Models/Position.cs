namespace FreeParkingSystem.Common.Models
{
    public class Position
    {
        public double Latitude { get; }

        public double Longitude { get; }

        public string Address { get; }

        public Position(double latitude, double longitude, string address)
        {
            Latitude = latitude;
            Longitude = longitude;
            Address = address;
        }

        public override string ToString()
        {
            return $"Lat: {Latitude}, Long: {Longitude}";
        }
    }
}

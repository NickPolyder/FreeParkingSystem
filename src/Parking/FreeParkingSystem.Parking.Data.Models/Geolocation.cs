namespace FreeParkingSystem.Parking.Data.Models
{
	public struct Geolocation	
	{
		public string X { get; }
		public string Y { get; }

		public Geolocation(string x, string y)
		{
			X = x;
			Y = y;
		}
	}
}
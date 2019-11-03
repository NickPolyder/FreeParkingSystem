namespace FreeParkingSystem.Parking.Contract
{
	public interface IParkingSiteServices
	{
		ParkingSite Get(int parkingSiteId);

		ParkingSite Add(ParkingSite parking);

		ParkingSite Update(ParkingSite parking);
	}
}
using System.Collections.Generic;

namespace FreeParkingSystem.Parking.Contract
{
	public interface IParkingSpotTypeServices
	{
		IEnumerable<ParkingSpotType> GetAll();
	}
}
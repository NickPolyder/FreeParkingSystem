using System.Collections.Generic;

namespace FreeParkingSystem.Parking.Contract
{
	public interface IParkingTypeServices
	{
		IEnumerable<ParkingType> GetAll();
	}
}
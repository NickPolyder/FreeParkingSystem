using System.Collections.Generic;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Repositories;

namespace FreeParkingSystem.Parking
{
	public class ParkingSpotTypeServices: IParkingSpotTypeServices
	{
		private readonly IParkingSpotTypeRepository _parkingSpotTypeRepository;

		public ParkingSpotTypeServices(IParkingSpotTypeRepository parkingSpotTypeRepository)
		{
			_parkingSpotTypeRepository = parkingSpotTypeRepository;
		}

		public IEnumerable<ParkingSpotType> GetAll()
		{
			return _parkingSpotTypeRepository.GetAll();
		}
	}
}
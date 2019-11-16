using System.Collections.Generic;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Repositories;
using FreeParkingSystem.Parking.Data.Models;

namespace FreeParkingSystem.Parking
{
	public class ParkingTypeServices : IParkingTypeServices
	{
		private readonly IParkingTypeRepository _parkingTypeRepository;

		public ParkingTypeServices(IParkingTypeRepository parkingTypeRepository)
		{
			_parkingTypeRepository = parkingTypeRepository;
		}

		public IEnumerable<ParkingType> GetAll()
		{
			return _parkingTypeRepository.GetAll();
		}
	}
}
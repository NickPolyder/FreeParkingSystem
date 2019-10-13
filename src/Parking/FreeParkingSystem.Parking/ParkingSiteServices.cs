using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Exceptions;
using FreeParkingSystem.Parking.Contract.Repositories;

namespace FreeParkingSystem.Parking
{
	public class ParkingSiteServices : IParkingSiteServices
	{
		private readonly IParkingSiteRepository _parkingSiteRepository;
		private readonly IParkingTypeRepository _parkingTypeRepository;

		public ParkingSiteServices(IParkingSiteRepository parkingSiteRepository,
			IParkingTypeRepository parkingTypeRepository)
		{
			_parkingSiteRepository = parkingSiteRepository;
			_parkingTypeRepository = parkingTypeRepository;
		}

		public ParkingSite Get(int parkingSiteId)
		{
			if (parkingSiteId < 1)
				return null;

			return _parkingSiteRepository.Get(parkingSiteId);
		}

		public ParkingSite Add(ParkingSite parking)
		{
			if (parking.Id > 0)
				throw new ParkingException();

			if (string.IsNullOrWhiteSpace(parking.Name))
				throw new ParkingException();

			if (_parkingSiteRepository.Exists(parking.Name))
				throw new ParkingException();

			if (!_parkingTypeRepository.Exists(parking.ParkingTypeId))
				throw new ParkingException();

			return _parkingSiteRepository.Add(parking);
		}
	}
}
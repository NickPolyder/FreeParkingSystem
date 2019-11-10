using System.Collections.Generic;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Exceptions;
using FreeParkingSystem.Parking.Contract.Repositories;

namespace FreeParkingSystem.Parking
{
	public class ParkingSpotServices : IParkingSpotServices
	{
		private readonly IParkingSpotRepository _parkingSpotRepository;
		private readonly IParkingSpotTypeRepository _parkingSpotTypeRepository;

		public ParkingSpotServices(IParkingSpotRepository parkingSpotRepository,
			IParkingSpotTypeRepository parkingSpotTypeRepository)
		{
			_parkingSpotRepository = parkingSpotRepository;
			_parkingSpotTypeRepository = parkingSpotTypeRepository;
		}

		public IEnumerable<ParkingSpot> GetViews(int parkingSiteId)
		{
			if (parkingSiteId < 0)
				return new List<ParkingSpot>(0);

			return _parkingSpotRepository.GetAllBy(parkingSiteId);
		}

		public ParkingSpot GetView(int parkingSpotId, int parkingSiteId)
		{
			if (parkingSpotId < 0 || parkingSiteId < 0)
				return null;

			return _parkingSpotRepository.GetBy(parkingSpotId, parkingSiteId);
		}

		public ParkingSpot Get(int parkingSpotId)
		{
			if (parkingSpotId < 1)
				return null;

			return _parkingSpotRepository.Get(parkingSpotId);
		}

		public ParkingSpot Add(ParkingSpot parkingSpot)
		{
			if (parkingSpot.Id > 0)
				throw new ParkingException(Contract.Resources.Validation.ParkingSpot_CannotUseAddWIthId);

			if (parkingSpot.ParkingSiteId < 1)
				throw new ParkingException(Contract.Resources.Validation.ParkingSpot_ParkingSiteIsNotValid);

			if (_parkingSpotRepository.Exists(parkingSpot))
				throw new ParkingException(Contract.Resources.Validation.ParkingSpot_AlreadyExists);

			if (!_parkingSpotTypeRepository.Exists(parkingSpot.ParkingSpotTypeId))
				throw new ParkingException(Contract.Resources.Validation.ParkingSpot_TypeDoesNotExist);

			parkingSpot.ParkingSite = null;
			parkingSpot.ParkingSpotType = null;
			return _parkingSpotRepository.Add(parkingSpot);
		}

		public ParkingSpot Update(ParkingSpot parkingSpot)
		{
			if (parkingSpot.Id == 0)
				throw new ParkingException(Contract.Resources.Validation.ParkingSpot_CannotUseUpdateWithNoId);

			if (parkingSpot.ParkingSiteId < 1)
				throw new ParkingException(Contract.Resources.Validation.ParkingSpot_ParkingSiteIsNotValid);

			if (!_parkingSpotRepository.Exists(parkingSpot))
				throw new ParkingException(Contract.Resources.Validation.ParkingSpot_DoesNotExist);

			if (!_parkingSpotTypeRepository.Exists(parkingSpot.ParkingSpotTypeId))
				throw new ParkingException(Contract.Resources.Validation.ParkingSpot_TypeDoesNotExist);

			parkingSpot.ParkingSite = null;
			parkingSpot.ParkingSpotType = null;
			return _parkingSpotRepository.Update(parkingSpot);
		}

		public void Delete(int parkingSpotId)
		{
			if (parkingSpotId <= 0)
				throw new ParkingException(Contract.Resources.Validation.ParkingSpot_CannotUseDeleteWithNoId);

			_parkingSpotRepository.Delete(parkingSpotId);
		}

		public void DeleteBySiteId(int parkingSiteId)
		{
			if (parkingSiteId <= 0)
				throw new ParkingException(Contract.Resources.Validation.ParkingSite_CannotUseDeleteWithNoId);

			_parkingSpotRepository.DeleteBySiteId(parkingSiteId);
		}
	}
}
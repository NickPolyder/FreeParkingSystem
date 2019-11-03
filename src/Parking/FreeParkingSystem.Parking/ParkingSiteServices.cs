﻿using FreeParkingSystem.Parking.Contract;
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
				throw new ParkingException(Contract.Resources.Validation.ParkingSite_CannotUseAddWIthId);

			if (string.IsNullOrWhiteSpace(parking.Name))
				throw new ParkingException(Contract.Resources.Validation.ParkingSite_NameCannotBeNullOrEmpty);

			if (_parkingSiteRepository.Exists(parking.Name))
				throw new ParkingException(Contract.Resources.Validation.ParkignSite_NameAlreadyExists);

			if (!_parkingTypeRepository.Exists(parking.ParkingTypeId))
				throw new ParkingException(Contract.Resources.Validation.ParkingSite_TypeDoesNotExist);

			return _parkingSiteRepository.Add(parking);
		}

		public ParkingSite Update(ParkingSite parking)
		{
			if (parking.Id == 0)
				throw new ParkingException(Contract.Resources.Validation.ParkingSite_CannotUseUpdateWithNoId);

			if (string.IsNullOrWhiteSpace(parking.Name))
				throw new ParkingException(Contract.Resources.Validation.ParkingSite_NameCannotBeNullOrEmpty);

			if (_parkingSiteRepository.Exists(parking.Name))
				throw new ParkingException(Contract.Resources.Validation.ParkignSite_NameAlreadyExists);

			if (!_parkingTypeRepository.Exists(parking.ParkingTypeId))
				throw new ParkingException(Contract.Resources.Validation.ParkingSite_TypeDoesNotExist);

			return _parkingSiteRepository.Update(parking);
		}
	}
}
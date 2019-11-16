using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.Messages;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Commands;
using FreeParkingSystem.Parking.Contract.Exceptions;
using FreeParkingSystem.Parking.Data.Models;
using MediatR;

namespace FreeParkingSystem.Parking.Commands
{
	public class AddParkingSpotHandler: IRequestHandler<AddParkingSpotRequest,BaseResponse>
	{
		private readonly IParkingSiteServices _parkingSiteServices;
		private readonly IParkingSpotServices _parkingSpotServices;
		private readonly IUserContextAccessor _userContextAccessor;

		public AddParkingSpotHandler(
			IParkingSiteServices parkingSiteServices,
			IParkingSpotServices parkingSpotServices,
			IUserContextAccessor userContextAccessor)
		{
			_parkingSiteServices = parkingSiteServices;
			_parkingSpotServices = parkingSpotServices;
			_userContextAccessor = userContextAccessor;
		}

		public Task<BaseResponse> Handle(AddParkingSpotRequest request, CancellationToken cancellationToken)
		{
			var user = _userContextAccessor.GetUserContext().UserToken;
			var userId = user.Get<int>(UserClaimTypes.Id);
			var parkingSite = _parkingSiteServices.Get(request.ParkingSiteId);

			if(parkingSite == null || !parkingSite.IsActive)
				return request.ToNotFoundResponse(new ParkingException(Contract.Resources.Validation.ParkingSite_DoesNotExist)).AsTask();

			if(parkingSite.OwnerId != userId)
				return request.ToValidationResponse(new ParkingException(Contract.Resources.Validation.ParkingSpot_IsNotTheOwner)).AsTask();

			var parkingSpot = new ParkingSpot
			{
				ParkingSiteId = request.ParkingSiteId,
				ParkingSite = parkingSite,
				Position = request.Position,
				Level = request.Level,
				ParkingSpotTypeId = request.ParkingSpotTypeId,
				IsAvailable = true,
			};

			var result = _parkingSpotServices.Add(parkingSpot);

			return request.ToCreatedResponse(result.Id, result).AsTask();
		}
	}
}
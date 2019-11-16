using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Common;
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
	public class ChangeParkingSpotHandler : IRequestHandler<ChangeParkingSpotRequest, BaseResponse>
	{
		private readonly IParkingSiteServices _parkingSiteServices;
		private readonly IParkingSpotServices _parkingSpotServices;
		private readonly IUserContextAccessor _userContextAccessor;

		public ChangeParkingSpotHandler(
			IParkingSiteServices parkingSiteServices,
			IParkingSpotServices parkingSpotServices,
			IUserContextAccessor userContextAccessor)
		{
			_parkingSiteServices = parkingSiteServices;
			_parkingSpotServices = parkingSpotServices;
			_userContextAccessor = userContextAccessor;
		}

		public Task<BaseResponse> Handle(ChangeParkingSpotRequest request, CancellationToken cancellationToken)
		{
			var user = _userContextAccessor.GetUserContext().UserToken;
			var userId = user.Get<int>(UserClaimTypes.Id);
			var currentParkingSpot = _parkingSpotServices.Get(request.Id);

			if (currentParkingSpot == null)
				return request.ToNotFoundResponse(new ParkingException(Contract.Resources.Validation.ParkingSpot_DoesNotExist)).AsTask();

			var parkingSite = _parkingSiteServices.Get(currentParkingSpot.ParkingSiteId);

			if (parkingSite == null || !parkingSite.IsActive)
				return request.ToNotFoundResponse(new ParkingException(Contract.Resources.Validation.ParkingSite_DoesNotExist)).AsTask();

			if (parkingSite.OwnerId != userId)
				return request.ToValidationResponse(new ParkingException(Contract.Resources.Validation.ParkingSpot_IsNotTheOwner)).AsTask();

			var patchComposite = new List<IPropertyPatch<ParkingSpot>>
			{
				currentParkingSpot.CreatePatch(obj => obj.Position),
				currentParkingSpot.CreatePatch(obj => obj.Level),
				currentParkingSpot.CreatePatch(obj => obj.ParkingSpotTypeId),
				currentParkingSpot.CreatePatch(obj => obj.IsAvailable)
			};

			var newParkingSpot = new ParkingSpot
			{
				ParkingSiteId = currentParkingSpot.ParkingSiteId,
				ParkingSite = parkingSite,
				Position = request.Position ?? currentParkingSpot.Position,
				Level = request.Level ?? currentParkingSpot.Level,
				ParkingSpotTypeId = request.ParkingSpotTypeId ?? currentParkingSpot.ParkingSpotTypeId,
				IsAvailable = request.IsAvailable ?? currentParkingSpot.IsAvailable,
			};

			patchComposite.ForEach(item => item.Patch(newParkingSpot));

			if (patchComposite.Any(item => item.HasChanged))
			{
				currentParkingSpot.ParkingSite = null;
				currentParkingSpot.ParkingSpotType = null;
				_parkingSpotServices.Update(currentParkingSpot);
			}

			return request.ToSuccessResponse().AsTask();
		}
	}
}
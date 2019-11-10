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
using MediatR;

namespace FreeParkingSystem.Parking.Commands
{
	public class ChangeParkingSiteDetailsHandler : IRequestHandler<ChangeParkingSiteDetailsRequest, BaseResponse>
	{
		private readonly IParkingSiteServices _parkingSiteServices;
		private readonly IUserContextAccessor _userContextAccessor;

		public ChangeParkingSiteDetailsHandler(IParkingSiteServices parkingSiteServices,
			IUserContextAccessor userContextAccessor)
		{
			_parkingSiteServices = parkingSiteServices;
			_userContextAccessor = userContextAccessor;
		}
		public Task<BaseResponse> Handle(ChangeParkingSiteDetailsRequest request, CancellationToken cancellationToken)
		{
			var user = _userContextAccessor.GetUserContext().UserToken;
			var userId = user.Get<int>(UserClaimTypes.Id);
			var currentParkingSite = _parkingSiteServices.Get(request.ParkingSiteId);

			if (currentParkingSite == null)
				return request
					.ToNotFoundResponse(new ParkingException(Contract.Resources.Validation.ParkingSite_DoesNotExist))
					.AsTask();

			if (currentParkingSite.OwnerId != userId)
				return request
					.ToValidationResponse(new ParkingException(Contract.Resources.Validation.ParkingSite_IsNotTheOwner))
					.AsTask();
			
			var patchComposite = new List<IPropertyPatch<ParkingSite>>
			{
				currentParkingSite.CreatePatch(obj => obj.IsActive),
				currentParkingSite.CreatePatch(obj => obj.Name),
				currentParkingSite.CreatePatch(obj => obj.ParkingTypeId),
				currentParkingSite.CreatePatch(obj => obj.Geolocation)
			};

			var newParkingSite = new ParkingSite
			{
				IsActive = request.IsActive,
				ParkingTypeId = request.ParkingTypeId,
				Name = request.Name,
				Geolocation = request.Geolocation
			};

			patchComposite.ForEach(item => item.Patch(newParkingSite));

			if (patchComposite.Any(item => item.HasChanged))
			{
				_parkingSiteServices.Update(currentParkingSite);
			}

			return request.ToSuccessResponse().AsTask();
		}
	}
}
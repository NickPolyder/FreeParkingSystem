using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.Messages;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Commands;
using FreeParkingSystem.Parking.Contract.Exceptions;
using MediatR;

namespace FreeParkingSystem.Parking.Commands
{
	public class DeleteParkingSpotHandler : IRequestHandler<DeleteParkingSpotRequest, BaseResponse>
	{
		private readonly IOrderApiServices _orderApiServices;
		private readonly IParkingSpotServices _parkingSpotServices;
		private readonly IUserContextAccessor _userContextAccessor;

		public DeleteParkingSpotHandler(IOrderApiServices orderApiServices,
			IParkingSpotServices parkingSpotServices,
			IUserContextAccessor userContextAccessor)
		{
			_orderApiServices = orderApiServices;
			_parkingSpotServices = parkingSpotServices;
			_userContextAccessor = userContextAccessor;
		}

		public async Task<BaseResponse> Handle(DeleteParkingSpotRequest request, CancellationToken cancellationToken)
		{
			var userId = _userContextAccessor.GetUserContext().GetUserId();

			var parkingSpot = _parkingSpotServices.Get(request.Id);

			if (parkingSpot == null)
				return request.ToNotFoundResponse(new ParkingException(Contract.Resources.Validation.ParkingSpot_DoesNotExist));

			if (parkingSpot.ParkingSite != null && !parkingSpot.ParkingSite.IsActive)
				return request.ToNotFoundResponse(new ParkingException(Contract.Resources.Validation.ParkingSite_DoesNotExist));

			if (parkingSpot.ParkingSite != null && parkingSpot.ParkingSite.OwnerId != userId)
				return request.ToValidationResponse(new ParkingException(Contract.Resources.Validation.ParkingSpot_IsNotTheOwner));

			var hasActiveOrders = await _orderApiServices.ParkingSpotHasActiveOrders(request.Id, cancellationToken);

			if (hasActiveOrders)
				return request.ToValidationResponse(new ParkingException(Contract.Resources.Validation.ParkingSpot_HasActiveOrder));

			parkingSpot.ParkingSite = null;
			parkingSpot.ParkingSpotType = null;

			_parkingSpotServices.Delete(request.Id);

			return request.ToSuccessResponse();
		}
	}
}
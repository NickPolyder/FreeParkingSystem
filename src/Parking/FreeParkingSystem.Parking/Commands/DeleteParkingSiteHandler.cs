using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Accounts.Contract.Messages;
using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.MessageBroker.Contract;
using FreeParkingSystem.Common.Messages;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Commands;
using FreeParkingSystem.Parking.Contract.Exceptions;
using MediatR;

namespace FreeParkingSystem.Parking.Commands
{
	public class DeleteParkingSiteHandler : IRequestHandler<DeleteParkingSiteRequest, BaseResponse>
	{
		private readonly IParkingSiteServices _parkingSiteServices;
		private readonly IOrderApiServices _orderApiServices;
		private readonly IParkingSpotServices _parkingSpotServices;
		private readonly IPublishBroker _publishBroker;
		private readonly IUserContextAccessor _userContextAccessor;

		public DeleteParkingSiteHandler(IParkingSiteServices parkingSiteServices,
			IOrderApiServices orderApiServices,
			IParkingSpotServices parkingSpotServices,
			IPublishBroker publishBroker,
			IUserContextAccessor userContextAccessor)
		{
			_parkingSiteServices = parkingSiteServices;
			_orderApiServices = orderApiServices;
			_parkingSpotServices = parkingSpotServices;
			_publishBroker = publishBroker;
			_userContextAccessor = userContextAccessor;
		}

		public async Task<BaseResponse> Handle(DeleteParkingSiteRequest request, CancellationToken cancellationToken)
		{
			var user = _userContextAccessor.GetUserContext().UserToken;
			var userId = user.Get<int>(UserClaimTypes.Id);

			var parkingSite = _parkingSiteServices.Get(request.Id);

			if (parkingSite == null)
				return request.ToValidationResponse(new ParkingException(Contract.Resources.Validation.ParkingSite_DoesNotExist));

			if (parkingSite.OwnerId != userId)
				return request.ToValidationResponse(new ParkingException(Contract.Resources.Validation.ParkingSite_IsNotTheOwner));
			
			var hasActiveOrders = await _orderApiServices.ParkingSiteHasActiveOrders(request.Id,cancellationToken);

			if (hasActiveOrders)
				return request.ToValidationResponse(new ParkingException(Contract.Resources.Validation.ParkingSite_HasActiveOrder));

			_parkingSpotServices.DeleteBySiteId(request.Id);

			_parkingSiteServices.Delete(request.Id);

			_publishBroker.Publish(new UserDeletedParkingSiteMessage(parkingSite.OwnerId));

			return request.ToSuccessResponse();
		}
	}
}
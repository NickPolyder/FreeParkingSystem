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
	public class AddParkingSpotToFavoritesHandler : IRequestHandler<AddParkingSpotToFavoritesRequest, BaseResponse>
	{
		private readonly IFavoriteServices _favoriteServices;
		private readonly IUserContextAccessor _userContextAccessor;
		private readonly IParkingSpotServices _parkingSpotServices;

		public AddParkingSpotToFavoritesHandler(IFavoriteServices favoriteServices,
			IUserContextAccessor userContextAccessor,
			IParkingSpotServices parkingSpotServices)
		{
			_favoriteServices = favoriteServices;
			_userContextAccessor = userContextAccessor;
			_parkingSpotServices = parkingSpotServices;
		}

		public Task<BaseResponse> Handle(AddParkingSpotToFavoritesRequest request, CancellationToken cancellationToken)
		{
			var userId = _userContextAccessor.GetUserContext().GetUserId();
			var parkingSpot = _parkingSpotServices.Get(request.ParkingSpotId);

			if(parkingSpot == null)
				return request.ToNotFoundResponse(new ParkingException(Contract.Resources.Validation.ParkingSpot_DoesNotExist)).AsTask();

			_favoriteServices.AddFavorite(userId, parkingSpot);

			return request.ToSuccessResponse().AsTask();
		}
	}
}
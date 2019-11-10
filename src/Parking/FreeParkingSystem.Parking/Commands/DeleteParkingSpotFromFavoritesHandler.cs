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
	public class DeleteParkingSpotFromFavoritesHandler : IRequestHandler<DeleteParkingSpotFromFavoritesRequest, BaseResponse>
	{
		private readonly IFavoriteServices _favoriteServices;
		private readonly IUserContextAccessor _userContextAccessor;
		private readonly IParkingSpotServices _parkingSpotServices;

		public DeleteParkingSpotFromFavoritesHandler(IFavoriteServices favoriteServices,
			IUserContextAccessor userContextAccessor,
			IParkingSpotServices parkingSpotServices)
		{
			_favoriteServices = favoriteServices;
			_userContextAccessor = userContextAccessor;
			_parkingSpotServices = parkingSpotServices;
		}

		public Task<BaseResponse> Handle(DeleteParkingSpotFromFavoritesRequest request, CancellationToken cancellationToken)
		{
			var userContext = _userContextAccessor.GetUserContext();
			var userId = userContext.UserToken.Get<int>(UserClaimTypes.Id);
			var parkingSpot = _parkingSpotServices.Get(request.ParkingSpotId);

			if (parkingSpot == null)
				return request.ToNotFoundResponse(new ParkingException(Contract.Resources.Validation.ParkingSpot_DoesNotExist)).AsTask();

			_favoriteServices.RemoveFavorite(userId, parkingSpot);

			return request.ToSuccessResponse().AsTask();
		}

	}
}
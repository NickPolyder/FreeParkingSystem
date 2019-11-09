using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.Messages;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Commands;
using MediatR;

namespace FreeParkingSystem.Parking.Commands
{
	public class DeleteParkingSiteFromFavoritesHandler : IRequestHandler<DeleteParkingSiteFromFavoritesRequest, BaseResponse>
	{
		private readonly IFavoriteServices _favoriteServices;
		private readonly IUserContextAccessor _userContextAccessor;
		private readonly IParkingSiteServices _parkingSiteServices;

		public DeleteParkingSiteFromFavoritesHandler(IFavoriteServices favoriteServices,
			IUserContextAccessor userContextAccessor,
			IParkingSiteServices parkingSiteServices)
		{
			_favoriteServices = favoriteServices;
			_userContextAccessor = userContextAccessor;
			_parkingSiteServices = parkingSiteServices;
		}

		public Task<BaseResponse> Handle(DeleteParkingSiteFromFavoritesRequest request, CancellationToken cancellationToken)
		{
			var userContext = _userContextAccessor.GetUserContext();
			var userId = userContext.UserToken.Get<int>(UserClaimTypes.Id);
			var parkingSite = _parkingSiteServices.Get(request.ParkingSiteId);

			_favoriteServices.RemoveFavorite(userId, parkingSite);

			return request.ToSuccessResponse().AsTask();
		}

	}
}
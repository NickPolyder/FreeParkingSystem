using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Accounts.Contract.Commands;
using FreeParkingSystem.Accounts.Contract.Messages;
using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.MessageBroker.Contract;
using FreeParkingSystem.Common.Messages;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Commands;
using MediatR;

namespace FreeParkingSystem.Parking.Commands
{
	public class AddParkingSiteHandler : IRequestHandler<AddParkingSiteRequest, BaseResponse>
	{
		private readonly IParkingSiteServices _parkingSiteServices;
		private readonly IUserContextAccessor _userContextAccessor;
		private readonly IPublishBroker _publishBroker;

		public AddParkingSiteHandler(IParkingSiteServices parkingSiteServices,
			IUserContextAccessor userContextAccessor,
			IPublishBroker publishBroker)
		{
			_parkingSiteServices = parkingSiteServices;
			_userContextAccessor = userContextAccessor;
			_publishBroker = publishBroker;
		}
		public Task<BaseResponse> Handle(AddParkingSiteRequest request, CancellationToken cancellationToken)
		{
			var user = _userContextAccessor.GetUserContext().UserToken;
			var userId = user.Get<int>(UserClaimTypes.Id);
			var parking = new ParkingSite
			{
				Name = request.Name,
				IsActive = true,
				OwnerId = userId,
				ParkingTypeId = request.ParkingTypeId,
				Geolocation = request.Geolocation
			};

			var result = _parkingSiteServices.Add(parking);

			_publishBroker.Publish(new UserCreatedParkingSiteMessage(userId));
			return Task.FromResult(request.ToSuccessResponse(result));
		}
	}
}
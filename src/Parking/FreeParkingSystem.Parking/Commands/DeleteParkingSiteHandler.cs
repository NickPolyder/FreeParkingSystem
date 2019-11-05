using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Common.ExtensionMethods;
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

		public DeleteParkingSiteHandler(IParkingSiteServices parkingSiteServices,
			IOrderApiServices orderApiServices,
			IParkingSpotServices parkingSpotServices)
		{
			_parkingSiteServices = parkingSiteServices;
			_orderApiServices = orderApiServices;
			_parkingSpotServices = parkingSpotServices;
		}

		public async Task<BaseResponse> Handle(DeleteParkingSiteRequest request, CancellationToken cancellationToken)
		{
			var parkingSite = _parkingSiteServices.Get(request.Id);

			if (parkingSite == null)
				return request.ToValidationResponse(new ParkingException(Contract.Resources.Validation.ParkingSite_DoesNotExist));

			var hasActiveOrders = await _orderApiServices.HasActiveOrder(request.Id,cancellationToken);

			if (hasActiveOrders)
				return request.ToValidationResponse(new ParkingException(Contract.Resources.Validation.ParkingSite_HasActiveOrder));

			_parkingSpotServices.DeleteBySiteId(request.Id);

			_parkingSiteServices.Delete(request.Id);

			return request.ToSuccessResponse();
		}
	}
}
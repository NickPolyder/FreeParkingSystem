using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.Messages;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Exceptions;
using FreeParkingSystem.Parking.Contract.Queries;
using MediatR;

namespace FreeParkingSystem.Parking.Queries
{
	public class GetParkingSpotByIdHandler : IRequestHandler<GetParkingSpotByIdRequest, BaseResponse>
	{
		private readonly IParkingSpotServices _parkingSpotServices;

		public GetParkingSpotByIdHandler(IParkingSpotServices parkingSpotServices)
		{
			_parkingSpotServices = parkingSpotServices;
		}

		public Task<BaseResponse> Handle(GetParkingSpotByIdRequest request, CancellationToken cancellationToken)
		{
			var item = _parkingSpotServices.GetView(request.Id, request.ParkingSiteId);

			if (item == null)
				return request.ToNotFoundResponse(new ParkingException(Contract.Resources.Validation.ParkingSpot_DoesNotExist)).AsTask();

			item.ParkingSite = null;

			return request.ToSuccessResponse(item).AsTask();
		}
	}
}
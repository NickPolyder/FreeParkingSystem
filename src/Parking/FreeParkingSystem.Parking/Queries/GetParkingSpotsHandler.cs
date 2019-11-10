using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.Messages;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Queries;
using MediatR;

namespace FreeParkingSystem.Parking.Queries
{
	public class GetParkingSpotsHandler : IRequestHandler<GetParkingSpotsRequest, BaseResponse>
	{
		private readonly IParkingSpotServices _parkingSpotServices;

		public GetParkingSpotsHandler(IParkingSpotServices parkingSpotServices)
		{
			_parkingSpotServices = parkingSpotServices;
		}

		public Task<BaseResponse> Handle(GetParkingSpotsRequest request, CancellationToken cancellationToken)
		{
			var list = _parkingSpotServices.GetViews(request.ParkingSiteId).ToList();

			list.ForEach(item => item.ParkingSite = null);

			return request.ToSuccessResponse(list).AsTask();
		}
	}
}
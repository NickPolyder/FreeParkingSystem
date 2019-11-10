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
	public class GetParkingSpotTypesHandler : IRequestHandler<GetParkingSpotTypesRequest, BaseResponse>
	{
		private readonly IParkingSpotTypeServices _parkingSpotTypeServices;

		public GetParkingSpotTypesHandler(IParkingSpotTypeServices parkingSpotTypeServices)
		{
			_parkingSpotTypeServices = parkingSpotTypeServices;
		}

		public Task<BaseResponse> Handle(GetParkingSpotTypesRequest request, CancellationToken cancellationToken)
		{
			var list = _parkingSpotTypeServices.GetAll().ToList();

			return request.ToSuccessResponse(list).AsTask();
		}
	}
}
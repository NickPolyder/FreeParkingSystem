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
	public class GetParkingSitesHandler : IRequestHandler<GetParkingSitesRequest, BaseResponse>
	{
		private readonly IParkingSiteServices _parkingSiteServices;

		public GetParkingSitesHandler(IParkingSiteServices parkingSiteServices)
		{
			_parkingSiteServices = parkingSiteServices;
		}

		public Task<BaseResponse> Handle(GetParkingSitesRequest request, CancellationToken cancellationToken)
		{
			var list = _parkingSiteServices.GetViews().ToList();

			return request.ToSuccessResponse(list).AsTask();
		}
	}
}
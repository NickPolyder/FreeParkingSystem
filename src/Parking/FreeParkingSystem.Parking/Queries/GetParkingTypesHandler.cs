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
	public class GetParkingTypesHandler : IRequestHandler<GetParkingTypesRequest, BaseResponse>
	{
		private readonly IParkingTypeServices _parkingTypeServices;

		public GetParkingTypesHandler(IParkingTypeServices parkingTypeServices)
		{
			_parkingTypeServices = parkingTypeServices;
		}

		public Task<BaseResponse> Handle(GetParkingTypesRequest request, CancellationToken cancellationToken)
		{
			var list = _parkingTypeServices.GetAll().ToList();

			return request.ToSuccessResponse(list).AsTask();
		}
	}
}
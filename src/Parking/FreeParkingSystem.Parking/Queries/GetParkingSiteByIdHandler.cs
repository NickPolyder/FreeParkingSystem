using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.Messages;
using FreeParkingSystem.Parking.Contract;
using FreeParkingSystem.Parking.Contract.Queries;
using MediatR;

namespace FreeParkingSystem.Parking.Queries
{
	public class GetParkingSiteByIdHandler : IRequestHandler<GetParkingSiteByIdRequest, BaseResponse>
	{
		private readonly IParkingSiteServices _parkingSiteServices;

		public GetParkingSiteByIdHandler(IParkingSiteServices parkingSiteServices)
		{
			_parkingSiteServices = parkingSiteServices;
		}

		public Task<BaseResponse> Handle(GetParkingSiteByIdRequest request, CancellationToken cancellationToken)
		{
			var item = _parkingSiteServices.GetView(request.Id);

			return request.ToSuccessResponse(item).AsTask();
		}
	}
}
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.Messages;
using FreeParkingSystem.Orders.Contract;
using FreeParkingSystem.Orders.Contract.Queries;
using MediatR;

namespace FreeParkingSystem.Orders.Queries
{
	public class GetActiveLeasesByParkingHandler : IRequestHandler<GetActiveLeasesByParkingRequest, BaseResponse>
	{
		private readonly IOrderServices _orderServices;
		private readonly IUserContextAccessor _userContextAccessor;

		public GetActiveLeasesByParkingHandler(IOrderServices orderServices,
			IUserContextAccessor userContextAccessor)
		{
			_orderServices = orderServices;
			_userContextAccessor = userContextAccessor;
		}

		public Task<BaseResponse> Handle(GetActiveLeasesByParkingRequest request, CancellationToken cancellationToken)
		{
			var userId = _userContextAccessor.GetUserContext().GetUserId();
			var items = _orderServices.GetActiveLeasesByParking(request.ParkingSiteId, userId).ToList();

			return request.ToSuccessResponse(items).AsTask();
		}
	}
}
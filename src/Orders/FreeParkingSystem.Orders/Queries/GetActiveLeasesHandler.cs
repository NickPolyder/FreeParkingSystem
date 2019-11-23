using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.Messages;
using FreeParkingSystem.Orders.Contract;
using FreeParkingSystem.Orders.Contract.Exceptions;
using FreeParkingSystem.Orders.Contract.Queries;
using MediatR;

namespace FreeParkingSystem.Orders.Queries
{
	public class GetActiveLeasesHandler : IRequestHandler<GetActiveLeasesRequest, BaseResponse>
	{
		private readonly IOrderServices _orderServices;
		private readonly IUserContextAccessor _userContextAccessor;

		public GetActiveLeasesHandler(IOrderServices orderServices, IUserContextAccessor userContextAccessor)
		{
			_orderServices = orderServices;
			_userContextAccessor = userContextAccessor;
		}


		public Task<BaseResponse> Handle(GetActiveLeasesRequest request, CancellationToken cancellationToken)
		{
			var userId = _userContextAccessor.GetUserContext().GetUserId();

			var items = _orderServices.GetViews(userId).ToList();

			return request.ToSuccessResponse(items).AsTask();
		}
	}
}
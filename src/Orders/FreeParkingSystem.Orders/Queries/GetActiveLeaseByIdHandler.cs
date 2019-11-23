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
	public class GetActiveLeaseByIdHandler : IRequestHandler<GetActiveLeaseByIdRequest, BaseResponse>
	{
		private readonly IOrderServices _orderServices;
		private readonly IUserContextAccessor _userContextAccessor;

		public GetActiveLeaseByIdHandler(IOrderServices orderServices, IUserContextAccessor userContextAccessor)
		{
			_orderServices = orderServices;
			_userContextAccessor = userContextAccessor;
		}


		public Task<BaseResponse> Handle(GetActiveLeaseByIdRequest request, CancellationToken cancellationToken)
		{
			var userId = _userContextAccessor.GetUserContext().GetUserId();

			var item = _orderServices.GetView(request.OrderId, userId);

			return item == null || item.IsCancelled || item.LeaseEndDate.HasValue
				? request.ToNotFoundResponse(new OrderException(Contract.Resources.Validations.Order_DoesNotExist)).AsTask()
				: request.ToSuccessResponse(item).AsTask();
		}
	}
}
using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Contract.Queries;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.Messages;
using MediatR;

namespace FreeParkingSystem.Accounts.Queries
{
	public class UserLoginHandler : IRequestHandler<UserLoginRequest, BaseResponse>
	{
		private readonly IUserServices _userServices;

		public UserLoginHandler(IUserServices userServices)
		{
			_userServices = userServices;
		}

		public Task<BaseResponse> Handle(UserLoginRequest request, CancellationToken cancellationToken)
		{
			var user = _userServices.Login(request.Username, request.Password);
			user.Password = Password.Empty;
			return Task.FromResult(request.ToSuccessResponse(user));
		}
	}
}
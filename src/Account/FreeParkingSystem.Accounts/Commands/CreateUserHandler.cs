using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Contract.Commands;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.Messages;
using MediatR;

namespace FreeParkingSystem.Accounts.Commands
{
	public class CreateUserHandler : IRequestHandler<CreateUserRequest, BaseResponse>
	{
		private readonly IUserServices _userServices;

		public CreateUserHandler(IUserServices userServices)
		{
			_userServices = userServices;
		}

		public Task<BaseResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
		{
			var user = _userServices.CreateUser(request.UserName, request.Password);

			if (!string.IsNullOrWhiteSpace(request.Email))
				_userServices.AddClaim(user, ClaimTypes.Email.ToString(), request.Email);

			_userServices.AddClaim(user, ClaimTypes.Role.ToString(), request.Role.ToString());
			
			return Task.FromResult(request.ToSuccessResponse());
		}
	}
}
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Contract.Queries;
using FreeParkingSystem.Common;
using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.Messages;
using MediatR;

namespace FreeParkingSystem.Accounts.Queries
{
	public class UserLoginHandler : IRequestHandler<UserLoginRequest, BaseResponse>
	{
		private readonly IUserServices _userServices;
		private readonly IAuthenticationServices _authenticationServices;
		private readonly IMap<UserClaim, Claim> _claimMapper;

		public UserLoginHandler(IUserServices userServices,
			IAuthenticationServices authenticationServices,
			IMap<UserClaim, Claim> claimMapper)
		{
			_userServices = userServices;
			_authenticationServices = authenticationServices;
			_claimMapper = claimMapper;
		}

		public Task<BaseResponse> Handle(UserLoginRequest request, CancellationToken cancellationToken)
		{
			var user = _userServices.Login(request.Username, request.Password);
			user.Password = Password.Empty;

			var userToken = _authenticationServices.CreateToken(user.UserName, Map(user));

			return Task.FromResult(request.ToSuccessResponse(userToken));
		}

		private IEnumerable<Claim> Map(User user)
		{
			var claims = _claimMapper.Map(user.Claims).ToList();
			claims.Add(new Claim(UserClaimTypes.Id.ToString(), user.Id.ToString()));

			return claims;
		}
	}
}
using System.Linq;
using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Contract.Messages;
using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.MessageBroker.Contract;
using Microsoft.Extensions.Logging;

namespace FreeParkingSystem.Accounts.Messages
{
	public class UserCreatedParkingSiteHandler : BaseMessageBrokerHandler<UserCreatedParkingSiteMessage>
	{
		private static readonly Role[] InvalidRoles = {
			Role.Admin,
			Role.Owner,
			Role.Anonymous
		};

		private readonly IUserServices _userServices;

		public UserCreatedParkingSiteHandler(ILoggerFactory loggerFactory, IUserServices userServices) : base(loggerFactory)
		{
			_userServices = userServices;
		}

		public override void Process(UserCreatedParkingSiteMessage message)
		{
			var user = _userServices.GetById(message.UserId);
			if (user == null)
			{
				Logger.LogWarning(Contract.Resources.Messages.User_DoesNotExist.WithArgs(message.UserId));
				return;
			}

			var currentRole = user.GetRole();
			if (InvalidRoles.Contains(currentRole))
			{
				Logger.LogWarning(Contract.Resources.Messages
					.User_DoesNotHaveTheCorrectRole
					.WithArgs(message.UserId, currentRole, Role.Owner));
				return;
			}

			_userServices.ChangeClaim(user, UserClaimTypes.Role.ToString(), Role.Owner.ToString());

			Logger.LogInformation(Contract.Resources.Messages.User_ClaimChangedSuccessfully.WithArgs(user.Id, Role.Owner));
		}
	}
}
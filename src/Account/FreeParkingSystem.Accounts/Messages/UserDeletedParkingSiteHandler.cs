using FreeParkingSystem.Accounts.Contract;
using FreeParkingSystem.Accounts.Contract.Messages;
using FreeParkingSystem.Common.Authorization;
using FreeParkingSystem.Common.ExtensionMethods;
using FreeParkingSystem.Common.MessageBroker.Contract;
using Microsoft.Extensions.Logging;

namespace FreeParkingSystem.Accounts.Messages
{
	public class UserDeletedParkingSiteHandler : BaseMessageBrokerHandler<UserDeletedParkingSiteMessage>
	{
		private readonly IUserServices _userServices;

		public UserDeletedParkingSiteHandler(ILoggerFactory loggerFactory, IUserServices userServices) : base(loggerFactory)
		{
			_userServices = userServices;
		}

		public override void Process(UserDeletedParkingSiteMessage message)
		{
			var user = _userServices.GetById(message.UserId);
			if (user == null)
			{
				Logger.LogWarning(Contract.Resources.Messages.User_DoesNotExist.WithArgs(message.UserId));
				return;
			}

			var currentRole = user.GetRole();
			if (currentRole != Role.Owner)
			{
				Logger.LogWarning(Contract.Resources.Messages
					.User_DoesNotHaveTheCorrectRole
					.WithArgs(message.UserId, currentRole, Role.Member));
				return;
			}

			_userServices.ChangeClaim(user, UserClaimTypes.Role.ToString(), Role.Member.ToString());

			Logger.LogInformation(Contract.Resources.Messages.User_ClaimChangedSuccessfully.WithArgs(user.Id, Role.Member));
		}
	}
}
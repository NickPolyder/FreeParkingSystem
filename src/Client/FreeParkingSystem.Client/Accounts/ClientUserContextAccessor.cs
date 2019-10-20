using FreeParkingSystem.Client.Infrastructure;
using FreeParkingSystem.Common.Authorization;

namespace FreeParkingSystem.Client.Accounts
{
	public class ClientUserContextAccessor : IUserContextAccessor
	{
		private readonly IAppDomainAccessor _appDomainAccessor;

		public ClientUserContextAccessor(IAppDomainAccessor appDomainAccessor)
		{
			_appDomainAccessor = appDomainAccessor;
		}

		public IUserContext GetUserContext()
		{
			var userToken = _appDomainAccessor.GetOrCreate(AccountConstants.UserKey,()=> UserToken.Empty);
			return new DefaultUserContext(userToken);
		}
	}
}
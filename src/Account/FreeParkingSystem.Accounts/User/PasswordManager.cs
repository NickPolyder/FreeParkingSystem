using FreeParkingSystem.Accounts.Contract.User;
using FreeParkingSystem.Common;

namespace FreeParkingSystem.Accounts
{
	public class PasswordManager : IPasswordManager
	{
		private readonly IHash<string> _stringHasher;
		private readonly IEncrypt<string> _stringEncrypter;

		public PasswordManager(IHash<string> stringHasher,IEncrypt<string> stringEncrypter)
		{
			_stringHasher = stringHasher;
			_stringEncrypter = stringEncrypter;
		}
		public Password CreatePassword(string password)
		{
			throw new System.NotImplementedException();
		}
	}
}
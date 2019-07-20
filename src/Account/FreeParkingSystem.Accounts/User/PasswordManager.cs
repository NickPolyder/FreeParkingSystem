using FreeParkingSystem.Accounts.Contract.User;
using FreeParkingSystem.Common;

namespace FreeParkingSystem.Accounts
{
	public class PasswordManager : IPasswordManager
	{
		private readonly IValidate<Password> _passwordValidator;
		private readonly IHash<Password> _passwordHasher;
		private readonly IEncrypt<Password> _passwordEncrypter;

		public PasswordManager(IValidate<Password> passwordValidator, IHash<Password> passwordHasher, IEncrypt<Password> passwordEncrypter)
		{
			_passwordValidator = passwordValidator;
			_passwordHasher = passwordHasher;
			_passwordEncrypter = passwordEncrypter;
		}
		public Password CreatePassword(string password)
		{
			var pass = new Password(password);
			_passwordValidator.Validate(pass);
		}
	}
}
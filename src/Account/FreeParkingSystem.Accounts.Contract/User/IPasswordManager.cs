namespace FreeParkingSystem.Accounts.Contract.User
{
	public interface IPasswordManager
	{

		/// <summary>
		/// Validates the given password and encrypts it.
		/// </summary>
		/// <param name="password"></param>
		/// <returns></returns>
		Password CreatePassword(string password);

	}
}
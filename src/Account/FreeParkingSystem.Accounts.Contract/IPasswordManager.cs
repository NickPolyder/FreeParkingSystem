namespace FreeParkingSystem.Accounts.Contract
{
	public interface IPasswordManager
	{

		/// <summary>
		/// Validates the given password and encrypts it.
		/// </summary>
		/// <param name="password"></param>
		/// <returns></returns>
		Password Create(string password);

		bool Verify(Password password, Password otherPassword);

	}
}
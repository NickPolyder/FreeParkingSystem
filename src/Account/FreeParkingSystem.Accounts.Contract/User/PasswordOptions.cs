namespace FreeParkingSystem.Accounts.Contract.User
{
	public class PasswordOptions
	{
		public int MinimumCharacters { get; }

		public int MaximumCharacters { get; }

		public PasswordRequirements Requirements { get; }

		public string AllowedSpecialCharacters { get; }


		public PasswordOptions(int minimumCharacters, int maximumCharacters, PasswordRequirements requirements, string allowedSpecialCharacters = null)
		{
			MinimumCharacters = minimumCharacters;
			MaximumCharacters = maximumCharacters;
			Requirements = requirements;
			AllowedSpecialCharacters = allowedSpecialCharacters;
		}

	}
}
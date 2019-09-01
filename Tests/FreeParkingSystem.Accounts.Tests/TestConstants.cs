using FreeParkingSystem.Accounts.Contract.Options;
using FreeParkingSystem.Testing;

namespace FreeParkingSystem.Accounts.Tests
{
	public class TestConstants : CommonTestsConstants
	{
		public const int MinimumCharacters = 0;

		public const int MaximumCharacters = int.MaxValue;

		public const PasswordRequirements DefaultPasswordRequirements = PasswordRequirements.None;
	}
}
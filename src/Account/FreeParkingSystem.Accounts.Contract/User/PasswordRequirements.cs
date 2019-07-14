using System;

namespace FreeParkingSystem.Accounts.Contract.User
{
	[Flags]
	public enum PasswordRequirements : byte
	{
		None,
		Special,
		Numbers,
		Capitals
	}
}
namespace FreeParkingSystem.Accounts.Contract
{
	public class UserClaim
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		public string Type { get; set; }

		public string Value { get; set; }

	}
}
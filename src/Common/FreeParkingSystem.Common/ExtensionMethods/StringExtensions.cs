namespace FreeParkingSystem.Common.ExtensionMethods
{
	public static class StringExtensions
	{

		public static string WithArgs(this string value, params object[] args)
			=> value == null ? null : string.Format(value, args);
	}
}
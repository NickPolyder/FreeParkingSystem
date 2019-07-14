using System;

namespace FreeParkingSystem.Common.Hashing
{
	public class ShaStringHasher : IHash<string>
	{
		private readonly IHash<byte[]> _byteHasher;

		public ShaStringHasher(IHash<byte[]> byteHasher)
		{
			_byteHasher = byteHasher;
		}

		public string Hash(string input)
		{
			var bytesToHash = System.Text.Encoding.UTF8.GetBytes(input);
			var hashedBytes = _byteHasher.Hash(bytesToHash);

			return Convert.ToBase64String(hashedBytes, Base64FormattingOptions.None);
		}
	}
}
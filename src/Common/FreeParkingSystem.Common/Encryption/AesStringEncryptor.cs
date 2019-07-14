using System;

namespace FreeParkingSystem.Common.Encryption
{
	public class AesStringEncryptor : IEncrypt<string>
	{
		private readonly IEncrypt<byte[]> _byteEncryptor;

		protected AesStringEncryptor(IEncrypt<byte[]> byteEncryptor)
		{
			_byteEncryptor = byteEncryptor;
		}

		public string Encrypt(string input)
		{
			var bytesToEncrypt = System.Text.Encoding.UTF8.GetBytes(input);
			var encryptedItem = _byteEncryptor.Encrypt(bytesToEncrypt);

			return Convert.ToBase64String(encryptedItem, Base64FormattingOptions.None);
		}

		public string Decrypt(string input)
		{
			var bytesToDecrypt = System.Text.Encoding.UTF8.GetBytes(input);
			var decryptedItem = _byteEncryptor.Decrypt(bytesToDecrypt);

			return Convert.ToBase64String(decryptedItem, Base64FormattingOptions.None);
		}
	}
}
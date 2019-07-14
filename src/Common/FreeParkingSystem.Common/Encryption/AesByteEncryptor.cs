using System;
using System.IO;
using System.Security.Cryptography;

namespace FreeParkingSystem.Common.Encryption
{
	public class AesByteEncryptor : IEncrypt<byte[]>
	{
		private readonly EncryptionOptions _options;
		private readonly Aes _aes;
		private readonly int _ivLength;
		public AesByteEncryptor(EncryptionOptions options)
		{
			_options = options;
			_aes = Aes.Create();
			_ivLength = _aes.BlockSize / 8;
		}

		public byte[] Encrypt(byte[] input)
		{
			var iv = GenerateIv();
			using (var encryptor = _aes.CreateEncryptor(_options.SecretKey, iv))
			using (MemoryStream ms = new MemoryStream())
			{
				ms.Write(iv, 0, iv.Length);
				using (var cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
				{
					cryptoStream.Write(input, 0, input.Length);
					cryptoStream.FlushFinalBlock();
					return ms.ToArray();
				}
			}
		}

		public byte[] Decrypt(byte[] input)
		{
			var iv = new byte[_ivLength];
			Array.Copy(input, 0, iv, 0, iv.Length);

			using (var decryptor = _aes.CreateDecryptor(_options.SecretKey, iv))
			using (MemoryStream ms = new MemoryStream())
			using (var cryptoStream = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
			{
				cryptoStream.Write(input, iv.Length, input.Length - iv.Length);
				cryptoStream.FlushFinalBlock();
				return ms.ToArray();
			}
		}

		private byte[] GenerateIv()
		{
			_aes.GenerateIV();
			return _aes.IV;
		}
	}
}
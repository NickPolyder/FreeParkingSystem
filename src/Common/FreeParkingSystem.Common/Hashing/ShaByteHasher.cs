using System.Security.Cryptography;

namespace FreeParkingSystem.Common.Hashing
{
	public class ShaByteHasher: IHash<byte[]>
	{
		private readonly SHA512 _sha512;
		public ShaByteHasher()
		{
			_sha512 = SHA512.Create();
		}

		public byte[] Hash(byte[] input)
		{
			_sha512.Initialize();
			var hashed = _sha512.ComputeHash(input);
			_sha512.Clear();
			return hashed;
		}
	}
}
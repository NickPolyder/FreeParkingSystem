namespace FreeParkingSystem.Common
{
	public class EncryptionOptions
	{

		public byte[] SecretKey { get; }


		public EncryptionOptions(byte[] secretKey)
		{
			SecretKey = secretKey;
		}

	}
}
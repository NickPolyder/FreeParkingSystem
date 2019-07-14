namespace FreeParkingSystem.Common
{
	public interface IEncrypt<TObject>
	{
		TObject Encrypt(TObject input);

		TObject Decrypt(TObject input);
	}
}
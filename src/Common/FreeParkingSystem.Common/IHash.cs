namespace FreeParkingSystem.Common
{
	public interface IHash<TObject>
	{
		TObject Hash(TObject input);
	}
}
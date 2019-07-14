namespace FreeParkingSystem.Common
{
	public interface IValidate<in TObject>
	{
		void Validate(TObject obj);
	}
}
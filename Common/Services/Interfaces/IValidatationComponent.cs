namespace FreeParkingSystem.Common.Services
{
    public interface IValidatationComponent
    {
        /// <summary>
        /// Validates the given object 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        IValidationResult Validate(object obj);
    }
}
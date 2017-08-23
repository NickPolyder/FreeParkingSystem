namespace FreeParkingSystem.Common.Services.Validation
{
    public interface IvalidationComponent
    {
        /// <summary>
        /// Validates the given object 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        IValidationResult Validate(object obj);
    }
}
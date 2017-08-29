// ReSharper disable once CheckNamespace
namespace FreeParkingSystem.Common.Services.Validation
{
    public interface IValidationComponent
    {
        /// <summary>
        /// Validates the given object 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        IValidationResult Validate(object obj);

        /// <summary>
        /// Determines if the object can be validated by this component
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool CanValidate(object obj);
    }
}
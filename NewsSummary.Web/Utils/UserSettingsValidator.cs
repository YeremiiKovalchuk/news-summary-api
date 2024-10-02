using System.ComponentModel.DataAnnotations;

namespace NewsSummary.Web.Utils;

public static class UserSettingsValidator
{
    /// <summary>
    /// Throws <see cref="ArgumentException"/> if object is not validated. Does nothing otherwise.
    /// </summary>
    /// <param name="obj"></param>
    /// <exception cref="ArgumentException"></exception>
    public static void Validate(IValidatableObject obj)
    {
        var validationResult = new List<ValidationResult>();
        if (!Validator.TryValidateObject(obj, new ValidationContext(obj), validationResult))
        {
            foreach (var validation in validationResult)
            {
                Console.WriteLine(validation.ErrorMessage);
            }
            throw new ArgumentException("Invalid Redis parameters");
        }
    }
}

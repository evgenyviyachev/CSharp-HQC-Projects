namespace ACTester.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using Utilities.Constants;

    public class PowerUsageAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int valueAsInt = (int)value;

            if (valueAsInt <= 0)
            {
                return new ValidationResult(string.Format(Constants.NonPositiveNumber, "Power Usage"));
            }

            return ValidationResult.Success;
        }
    }
}

namespace ACTester.Models.Attributes
{
    using Utilities.Constants;
    using System.ComponentModel.DataAnnotations;

    public class VolumeCoveredAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int valueAsInt = (int)value;

            if (valueAsInt <= 0)
            {
                return new ValidationResult(string.Format(Constants.NonPositiveNumber, "Volume Covered"));
            }

            return ValidationResult.Success;
        }
    }
}

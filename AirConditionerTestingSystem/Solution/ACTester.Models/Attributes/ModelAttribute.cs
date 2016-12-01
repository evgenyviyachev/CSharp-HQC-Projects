namespace ACTester.Models.Attributes
{
    using System.ComponentModel.DataAnnotations;
    using Utilities.Constants;

    public class ModelAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string valueAsString = (string)value;

            if (valueAsString.Length < Constants.ModelMinLength || valueAsString.Trim() == string.Empty)
            {
                return new ValidationResult(string.Format(Constants.IncorrectPropertyLength, "Model", Constants.ModelMinLength));
            }

            return ValidationResult.Success;
        }
    }
}

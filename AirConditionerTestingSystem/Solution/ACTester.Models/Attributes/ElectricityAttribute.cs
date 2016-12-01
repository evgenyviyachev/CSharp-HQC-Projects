namespace ACTester.Models.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Utilities.Constants;

    public class ElectricityUsedAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            int valueAsInt = (int)value;

            if (valueAsInt <= 0)
            {
                return new ValidationResult(string.Format(Constants.NonPositiveNumber, "Electricity Used"));
            }

            return ValidationResult.Success;
        }
    }
}

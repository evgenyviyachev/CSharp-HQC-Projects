namespace BankSystem.Models.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class EmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string emailString = (string)value;
            if (emailString == null)
            {
                throw new ArgumentException("The email is not of string type.");
            }

            string regularExpressionString = @"([a-zA-Z0-9][a-zA-Z_\-.]*[a-zA-Z0-9])@([a-zA-Z0-9][a-zA-Z-]*\.[a-zA-Z-]*[a-zA-Z0-9](\.[a-zA-Z-]+)*)";
            Regex regex = new Regex(regularExpressionString);
            if (!regex.IsMatch(emailString))
            {
                return false;
            }

            return true;
        }
    }
}

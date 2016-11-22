namespace BankSystem.Models.ValidationAttributes
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class UsernameAttribute : ValidationAttribute
    {
        private readonly int _minLength;

        public UsernameAttribute(int minLength = 3)
        {
            this._minLength = minLength;
        }

        public override bool IsValid(object value)
        {
            string username = (string)value;

            if (username == null)
            {
                throw new ArgumentException("Property must be of type string.");
            }

            if (username.Length < this._minLength)
            {
                return false;
            }

            string regularExpressionString = @"([a-zA-Z][a-zA-Z0-9]{2,})";
            Regex regex = new Regex(regularExpressionString);
            if (!regex.IsMatch(username))
            {
                return false;
            }

            return true;
        }
    }
}

namespace BankSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using ValidationAttributes;

    public partial class User
    {
        private User()
        {
            this.Accounts = new HashSet<Account>();
        }

        public User(string username, string password, string email)
        {
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.Accounts = new HashSet<Account>();
        }

        [Key]
        public int UserId { get; set; }

        [Required]
        [Username(ErrorMessage = "Incorrect username")]
        public string Username { get; set; }

        [Required]
        [Password(
            ShouldContainDigit = true, ShouldContainLowercase = true,
            ShouldContainSpecialSymbol = false, ShouldContainUppercase = true,
            ErrorMessage = "Incorrect password")]
        public string Password { get; set; }

        [Required]
        [Email(ErrorMessage = "Incorrect email")]
        public string Email { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
    }
}

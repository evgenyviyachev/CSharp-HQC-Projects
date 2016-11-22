namespace BankSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SavingsAccount : Account
    {
        private SavingsAccount()
        {
        }

        public SavingsAccount(decimal balance, decimal interestRate) 
            : base(balance)
        {
            this.InterestRate = interestRate;
        }

        [Required]
        public decimal InterestRate { get; private set; }

        public string AddInterest()
        {
            this.Balance += this.InterestRate * this.Balance;

            return $"Added interest to {this.AccountNumber}. Current balance: {this.Balance : 0.00}";
        }
    }
}

namespace BankSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CheckingAccount : Account
    {
        private CheckingAccount()
        {
        }

        public CheckingAccount(decimal balance, decimal fee)
            : base(balance)
        {
            this.Fee = fee;
        }

        [Required]
        public decimal Fee { get; private set; }

        public string DeductFee()
        {
            if (this.Balance < this.Fee)
            {
                return $"Account {this.AccountNumber} does not have sufficient funds to deduct fee";
            }

            this.Balance -= this.Fee;

            return $"Deducted fee of {this.AccountNumber}. Current balance: {this.Balance: 0.00}";
        }
    }
}

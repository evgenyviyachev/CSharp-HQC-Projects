namespace BankSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class Account
    {
        protected Account()
        {
        }

        public Account(decimal balance)
        {
            this.AccountNumber = this.GetRandomAccountNumber();
            this.Balance = balance;
        }
        
        [Key]
        public int AccountId { get; set; }
        
        public string AccountNumber { get; private set; }

        [Required]
        public decimal Balance { get; protected set; }

        public virtual User User { get; set; }

        public string DepositMoney(decimal money)
        {
            this.Balance += money;

            return $"Account {this.AccountNumber} has balance of {this.Balance}";
        }

        public string WithdrawMoney(decimal money)
        {
            if (this.Balance < money)
            {
                return $"Account {this.AccountNumber} does not have sufficient funds to withdraw this sum";
            }

            this.Balance -= money;

            return $"Account {this.AccountNumber} has balance of {this.Balance}";
        }

        private string GetRandomAccountNumber()
        {
            int accountNumberLength = 10;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var accountNumber = new char[accountNumberLength];
            Random random = new Random();

            for (int i = 0; i < accountNumberLength; i++)
            {
                accountNumber[i] = chars[random.Next(chars.Length)];
            }

            return new string(accountNumber);
        }
    }
}

namespace BankSystem.ConsoleClient.IO.Commands
{
    using Attributes;
    using Data;
    using Models;
    using System;

    [Alias("addsavingsaccount")]
    public class AddSavingsAccountCommand : Command
    {
        public AddSavingsAccountCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override string Execute()
        {
            using (var context = new BankSystemContext())
            {
                if (this.Data.Length != 3)
                {
                    throw new ArgumentException("Command is invalid!");
                }

                if (!User.IsLoggedIn)
                {
                    throw new InvalidOperationException("No user is logged in.");
                }

                decimal initialBalance = Convert.ToDecimal(this.Data[1]);
                decimal interestRate = Convert.ToDecimal(this.Data[2]);

                var user = context.Users.Find(User.LoggedUserId);
                var account = new SavingsAccount(initialBalance, interestRate);

                user.Accounts.Add(account);
                context.SaveChanges();
                
                return $"Succesfully added account with number {account.AccountNumber}";
            }
        }
    }
}

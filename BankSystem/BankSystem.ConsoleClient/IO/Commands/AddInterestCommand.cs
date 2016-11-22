namespace BankSystem.ConsoleClient.IO.Commands
{
    using Attributes;
    using Data;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    [Alias("addinterest")]
    public class AddInterestCommand : Command
    {
        public AddInterestCommand(string input, string[] data) 
            : base(input, data)
        {
        }

        public override string Execute()
        {
            using (var context = new BankSystemContext())
            {
                if (this.Data.Length != 2)
                {
                    throw new ArgumentException("Command is invalid!");
                }

                if (!User.IsLoggedIn)
                {
                    throw new InvalidOperationException("No user is logged in.");
                }

                string accountNumber = this.Data[1];

                var user = context.Users
                    .Include(u => u.Accounts)
                    .FirstOrDefault(u => u.UserId == User.LoggedUserId);
                var account = user.Accounts
                    .FirstOrDefault(a => a.AccountNumber == accountNumber);

                if (account == null)
                {
                    throw new ArgumentException("No such account for this user.");
                }

                var savingsAccount = account as SavingsAccount;

                if (savingsAccount == null)
                {
                    throw new ArgumentException("This account is not a savings account.");
                }

                string result = savingsAccount.AddInterest();

                context.SaveChanges();

                return result;
            }
        }
    }
}

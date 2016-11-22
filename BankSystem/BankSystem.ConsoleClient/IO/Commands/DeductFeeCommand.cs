namespace BankSystem.ConsoleClient.IO.Commands
{
    using Attributes;
    using Data;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    [Alias("deductfee")]
    public class DeductFeeCommand : Command
    {
        public DeductFeeCommand(string input, string[] data)
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

                var checkingAccount = account as CheckingAccount;

                if (checkingAccount == null)
                {
                    throw new ArgumentException("This account is not a checking account.");
                }

                string result = checkingAccount.DeductFee();

                context.SaveChanges();

                return result;
            }
        }
    }
}

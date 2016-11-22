namespace BankSystem.ConsoleClient.IO.Commands
{
    using Attributes;
    using Data;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    [Alias("deposit")]
    public class DepositCommand : Command
    {
        public DepositCommand(string input, string[] data)
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

                string accountNumber = this.Data[1];
                decimal money = Convert.ToDecimal(this.Data[2]);

                var user = context.Users
                    .Include(u => u.Accounts)
                    .FirstOrDefault(u => u.UserId == User.LoggedUserId);
                var account = user.Accounts
                    .FirstOrDefault(a => a.AccountNumber == accountNumber);

                if (account == null)
                {
                    throw new ArgumentException("No such account for this user.");
                }

                string result = account.DepositMoney(money);

                context.SaveChanges();

                return result;
            }
        }
    }
}

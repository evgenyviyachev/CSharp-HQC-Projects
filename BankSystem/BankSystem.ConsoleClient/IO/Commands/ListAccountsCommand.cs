namespace BankSystem.ConsoleClient.IO.Commands
{
    using Attributes;
    using Data;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;

    [Alias("listaccounts")]
    public class ListAccountsCommand : Command
    {
        public ListAccountsCommand(string input, string[] data)
            : base(input, data)
        {
        }

        public override string Execute()
        {
            using (var context = new BankSystemContext())
            {
                StringBuilder sb = new StringBuilder();

                if (this.Data.Length != 1)
                {
                    throw new ArgumentException("Command is invalid!");
                }

                if (!User.IsLoggedIn)
                {
                    throw new InvalidOperationException("No user is logged in.");
                }

                var user = context.Users
                    .Include(u => u.Accounts)
                    .FirstOrDefault(u => u.UserId == User.LoggedUserId);

                var savingAccounts = user.Accounts.Where(a => a is SavingsAccount);
                var checkingAccounts = user.Accounts.Where(a => a is CheckingAccount);

                sb.AppendLine("Saving Accounts:");

                if (savingAccounts.Count() > 0)
                {
                    foreach (var acc in savingAccounts)
                    {
                        sb.AppendLine($"---{acc.AccountNumber} {acc.Balance : 0.00}");
                    }
                }

                sb.AppendLine("Checking Accounts:");

                if (checkingAccounts.Count() > 0)
                {
                    foreach (var acc in checkingAccounts)
                    {
                        sb.AppendLine($"---{acc.AccountNumber} {acc.Balance: 0.00}");
                    }
                }

                sb = sb.Remove(sb.Length - 1, 1);

                return sb.ToString();
            }
        }
    }
}

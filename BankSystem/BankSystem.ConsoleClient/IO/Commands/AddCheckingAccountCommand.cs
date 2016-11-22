namespace BankSystem.ConsoleClient.IO.Commands
{
    using Attributes;
    using Data;
    using Models;
    using System;

    [Alias("addcheckingaccount")]
    public class AddCheckingAccountCommand : Command
    {
        public AddCheckingAccountCommand(string input, string[] data)
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
                decimal fee = Convert.ToDecimal(this.Data[2]);

                var user = context.Users.Find(User.LoggedUserId);
                var account = new CheckingAccount(initialBalance, fee);

                user.Accounts.Add(account);
                context.SaveChanges();

                return $"Succesfully added account with number {account.AccountNumber}";
            }
        }
    }
}

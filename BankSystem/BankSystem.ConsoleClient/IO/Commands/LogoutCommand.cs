namespace BankSystem.ConsoleClient.IO.Commands
{
    using Attributes;
    using Data;
    using Models;
    using System;

    [Alias("logout")]
    public class LogoutCommand : Command
    {
        public LogoutCommand(string input, string[] data) 
            : base(input, data)
        {
        }

        public override string Execute()
        {
            using (var context = new BankSystemContext())
            {
                if (this.Data.Length != 1)
                {
                    throw new ArgumentException("Command is invalid!");
                }

                if (!User.IsLoggedIn)
                {
                    throw new InvalidOperationException("Cannot log out. No user is logged in.");
                }

                var username = context.Users.Find(User.LoggedUserId).Username;

                User.IsLoggedIn = false;
                User.LoggedUserId = 0;

                return $"User {username} successfully logged out";
            }
        }
    }
}

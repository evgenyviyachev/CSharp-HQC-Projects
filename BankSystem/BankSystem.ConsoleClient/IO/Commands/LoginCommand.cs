namespace BankSystem.ConsoleClient.IO.Commands
{
    using Attributes;
    using Data;
    using Models;
    using System;
    using System.Linq;

    [Alias("login")]
    public class LoginCommand : Command
    {
        public LoginCommand(string input, string[] data)
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

                string username = this.Data[1];
                string password = this.Data[2];

                if (User.IsLoggedIn)
                {
                    throw new InvalidOperationException("A user is already logged in");
                }

                if (!context.Users.Any(u => u.Username == username
                    && u.Password == password))
                {
                    throw new InvalidOperationException("Incorrect username \\ password");
                }

                int loggedUserId = context.Users
                    .First(u => u.Username == username && u.Password == password)
                    .UserId;

                User.IsLoggedIn = true;
                User.LoggedUserId = loggedUserId;

                return $"Succesfully logged in {username}";
            }
        }
    }
}

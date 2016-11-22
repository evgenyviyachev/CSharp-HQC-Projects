namespace BankSystem.ConsoleClient.IO.Commands
{
    using Attributes;
    using Data;
    using Models;
    using System;
    using System.Data.Entity.Validation;
    using System.Linq;

    [Alias("register")]
    public class RegisterUserCommand : Command
    {
        public RegisterUserCommand(string input, string[] data) 
            : base(input, data)
        {
        }

        public override string Execute()
        {
            using (var context = new BankSystemContext())
            {
                if (this.Data.Length != 4)
                {
                    throw new ArgumentException("Command is invalid!");
                }

                string username = this.Data[1];
                string password = this.Data[2];
                string email = this.Data[3];

                try
                {
                    User user = new User(username, password, email);

                    if (context.Users.Any(u => u.Username == username))
                    {
                        throw new InvalidOperationException("Username is already taken");
                    }

                    context.Users.Add(user);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var ve in ex.EntityValidationErrors)
                    {
                        foreach (var e in ve.ValidationErrors)
                        {
                            return e.ErrorMessage;
                        }
                    }
                }
                
                return $"{username} was registered in the system";
            }
        }
    }
}

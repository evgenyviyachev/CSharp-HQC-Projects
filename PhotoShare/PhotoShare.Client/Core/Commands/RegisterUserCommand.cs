namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Attributes;
    using Data.Server;
    using Models;
    using Data.Services;

    public class RegisterUserCommand : Command
    {
        [Inject]
        private UserService userService;

        public RegisterUserCommand(string[] data)
            : base(data)
        {
        }

        //RegisterUser <username> <password> <repeat-password> <email>
        public override string Execute()
        {
            if (this.Data.Length != 5)
            {
                throw new ArgumentException("Inaccurate number of parameters for this command!");
            }

            string username = Data[1];
            string password = Data[2];
            string repeatPassword = Data[3];
            string email = Data[4];

            if (password == repeatPassword)
            {
                throw new InvalidOperationException("Passwords do not match!");
            }

            this.userService.RegisterUser(username, password, email);
                        
            return "User " + username + " was registered successfully";
        }
    }
}

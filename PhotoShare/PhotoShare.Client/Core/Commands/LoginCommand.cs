namespace PhotoShare.Client.Core.Commands
{
    using Attributes;
    using Data.Server;
    using Data.Services;
    using Models;
    using System;

    public class LoginCommand : Command
    {
        [Inject]
        private UserService userService;

        public LoginCommand(string[] data) 
            : base(data)
        {
        }

        public override string Execute()
        {
            //Login <username> <password>
            if (this.Data.Length != 3)
            {
                throw new ArgumentException("Inaccurate number of parameters for this command!");
            }

            string username = this.Data[1];
            string password = this.Data[2];

            this.userService.LoginUser(username, password);

            return "User " + username + " successfully logged in";
        }
    }
}

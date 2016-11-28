namespace PhotoShare.Client.Core.Commands
{
    using Attributes;
    using Data.Server;
    using Data.Services;
    using Models;
    using System;

    public class LogoutCommand : Command
    {
        [Inject]
        private UserService userService;

        public LogoutCommand(string[] data) 
            : base(data)
        {
        }

        public override string Execute()
        {
            //Logout
            if (this.Data.Length != 1)
            {
                throw new ArgumentException("Inaccurate number of parameters for this command!");
            }

            this.userService.Logout();

            return "Successfully logged out";
        }
    }
}

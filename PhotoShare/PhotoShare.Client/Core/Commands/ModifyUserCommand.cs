namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Attributes;
    using Data.Server;
    using Models;
    using Data.Services;

    public class ModifyUserCommand : Command
    {
        [Inject]
        private UserService userService;

        public ModifyUserCommand(string[] data)
            : base(data)
        {
        }

        //ModifyUser <username> <property> <new value>
        //For example:
        //ModifyUser <username> Password <NewPassword>
        //ModifyUser <username> Email <NewEmail>
        //ModifyUser <username> FirstName <NewFirstName>
        //ModifyUser <username> LastName <newLastName>
        //ModifyUser <username> BornTown <newBornTownName>
        //ModifyUser <username> CurrentTown <newCurrentTownName>
        //!!! Cannot change username
        public override string Execute()
        {
            if (this.Data.Length != 4)
            {
                throw new ArgumentException("Inaccurate number of parameters for this command!");
            }

            string username = this.Data[1];
            string userProperty = this.Data[2];
            string newValue = this.Data[3];

            this.userService.ChangeUserProperty(username, userProperty, newValue);

            return "User " + username + " has a property " + userProperty + " that was changed";
        }
    }
}

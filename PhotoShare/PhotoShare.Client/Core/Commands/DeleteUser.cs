namespace PhotoShare.Client.Core.Commands
{
    using Attributes;
    using Data.Server;
    using Data.Services;
    using Models;
    using System;
    public class DeleteUser : Command
    {
        [Inject]
        private UserService userService;

        public DeleteUser(string[] data)
            : base(data)
        {
        }

        //DeleteUser <username>
        public override string Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new ArgumentException("Inaccurate number of parameters for this command!");
            }

            //TODO Delete User by username (only mark him as inactive)
            string username = Data[1];

            this.userService.DeleteUserByUsername(username);
            
            return $"User {username} was deleted from the databse"; 
        }
    }
}

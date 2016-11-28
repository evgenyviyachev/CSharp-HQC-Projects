namespace PhotoShare.Client.Core.Commands
{
    using Attributes;
    using Data.Server;
    using Data.Services;
    using Models;
    using System;

    public class PrintFriendsListCommand : Command
    {
        [Inject]
        private UserService userService;

        public PrintFriendsListCommand(string[] data)
            : base(data)
        {
        }

        //PrintFriendsList <username>
        public override string Execute()
        {
            if (this.Data.Length != 2)
            {
                throw new ArgumentException("Inaccurate number of parameters for this command!");
            }

            string username = this.Data[1];

            return this.userService.GetFriendsOfUser(username);
        }
    }
}

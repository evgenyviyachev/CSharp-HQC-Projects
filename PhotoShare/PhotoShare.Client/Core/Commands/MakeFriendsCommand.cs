namespace PhotoShare.Client.Core.Commands
{
    using Attributes;
    using Data.Server;
    using Data.Services;
    using Models;
    using System;

    public class MakeFriendsCommand : Command
    {
        [Inject]
        private UserService userService;

        public MakeFriendsCommand(string[] data)
            : base(data)
        {
        }

        public override string Execute()
        {
            //bidirectional adding friends
            //MakeFriends <username1> <username2>
            if (this.Data.Length != 3)
            {
                throw new ArgumentException("Inaccurate number of parameters for this command!");
            }

            string username1 = this.Data[1];
            string username2 = this.Data[2];

            this.userService.MakeFriends(username1, username2);

            return "User " + username1 + " and user" + username2 + " became friends";
        }
    }
}

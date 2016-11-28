namespace PhotoShare.Client.Core.Commands
{
    using Attributes;
    using Data.Server;
    using Data.Services;
    using Models;
    using System;

    public class UploadProfilePictureCommand : Command
    {
        [Inject]
        private PictureService pictureService;

        public UploadProfilePictureCommand(string[] data)
            : base(data)
        {
        }

        //UploadProfilePicture <username> <pictureFilePath>
        public override string Execute()
        {
            if (this.Data.Length != 3)
            {
                throw new ArgumentException("Inaccurate number of parameters for this command!");
            }

            string username = this.Data[1];
            string picturePath = this.Data[2];

            this.pictureService.UploadProfilePicture(username, picturePath);

            return "User " + username + " successfully changed their profile picture";
        }
    }
}

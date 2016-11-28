namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Attributes;
    using Data.Server;
    using Models;
    using Data.Services;

    public class UploadPictureCommand : Command
    {
        [Inject]
        private PictureService pictureService;

        public UploadPictureCommand(string[] data)
            : base(data)
        {
        }

        //UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public override string Execute()
        {
            if (this.Data.Length != 4)
            {
                throw new ArgumentException("Inaccurate number of parameters for this command!");
            }

            string albumName = this.Data[1];
            string pictureTitle = this.Data[2];
            string picturePath = this.Data[3];

            this.pictureService.UploadPicture(albumName, pictureTitle, picturePath);

            return "Picture " + pictureTitle + " successfully uploaded to album " + albumName;
        }
    }
}

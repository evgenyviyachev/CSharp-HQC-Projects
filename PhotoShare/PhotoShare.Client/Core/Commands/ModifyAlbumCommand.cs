namespace PhotoShare.Client.Core.Commands
{
    using Attributes;
    using Data.Server;
    using Data.Services;
    using Models;
    using System;

    public class ModifyAlbumCommand : Command
    {
        [Inject]
        private AlbumService albumService;

        public ModifyAlbumCommand(string[] data)
            : base(data)
        {
        }

        //ModifyAlbum <albumId> <property> <new value>
        //For example
        //ModifyAlbum 4 Name <new name>
        //ModifyAlbum 4 BackgroundColor <new color>
        //ModifyAlbum 4 IsPublic <True/False>
        public override string Execute()
        {
            if (this.Data.Length != 4)
            {
                throw new ArgumentException("Inaccurate number of parameters for this command!");
            }

            int albumId;

            try
            {
                albumId = int.Parse(this.Data[1]);
            }
            catch (Exception)
            {
                throw new ArgumentException("AlbumId must be of type int!");
            }

            string albumProperty = this.Data[2];
            string newValue = this.Data[3];

            this.albumService.ChangeAlbumProperty(albumId, albumProperty, newValue);

            return "Album with id " + albumId + " has a property " + albumProperty + " that was changed";
        }
    }
}

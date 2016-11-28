namespace PhotoShare.Client.Core.Commands
{
    using Attributes;
    using Data.Server;
    using Data.Services;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class CreateAlbumCommand : Command
    {
        [Inject]
        private AlbumRoleService albumRoleService;

        public CreateAlbumCommand(string[] data)
            : base(data)
        {
        }

        //CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public override string Execute()
        {
            if (this.Data.Length < 4)
            {
                throw new ArgumentException("Inaccurate number of parameters for this command!");
            }

            string username = this.Data[1];
            string albumTitle = this.Data[2];
            string colorStr = this.Data[3];

            string[] tagNames = this.Data.Skip(4).ToArray();

            this.albumRoleService.CreateAlbum(username, albumTitle, colorStr, tagNames);

            return albumTitle + " album created with Owner " + username + ". Successfully added to database";
        }
    }
}

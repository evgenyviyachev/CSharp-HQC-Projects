namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Attributes;
    using Data.Server;
    using Models;
    using Data.Services;

    public class ShareAlbumCommand : Command
    {
        [Inject]
        private AlbumRoleService albumRoleService;

        public ShareAlbumCommand(string[] data) : base(data)
        {
        }

        //ShareAlbum <albumId> <username> <permission>
        //For example:
        //ShareAlbum 4 dragon321 Owner
        //ShareAlbum 4 dragon11 Viewer
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

            string username = this.Data[2];
            string permission = this.Data[3];

            this.albumRoleService.ShareAlbum(albumId, username, permission);

            return "User " + username + " now has a " + permission + " permission on album with id " + albumId;
        }
    }
}

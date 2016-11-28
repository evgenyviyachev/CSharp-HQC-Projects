namespace PhotoShare.Client.Core.Commands
{
    using Attributes;
    using Data.Server;
    using Data.Services;
    using Models;
    using System;

    public class AddTagToCommand : Command
    {
        [Inject]
        private AlbumService albumService;

        public AddTagToCommand(string[] data)
            : base(data)
        {
        }

        //AddTagTo <albumName> <tag>
        public override string Execute()
        {
            if (this.Data.Length != 3)
            {
                throw new ArgumentException("Inaccurate number of parameters for this command!");
            }

            string tagName = this.Data[2].ValidateOrTransform();
            string albumName = this.Data[1];

            this.albumService.AddTagToAlbum(new Tag() { Name = tagName }, albumName);

            return tagName + " was successfully added to album " + albumName;
        }
    }
}

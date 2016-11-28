namespace PhotoShare.Data.Services
{
    using Contracts;
    using Models;
    using System;
    using System.Linq;
    using System.Reflection;

    public class AlbumService
    {
        private IUnitOfWork unitOfWork;

        public AlbumService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AddTagToAlbum(Tag tag, string albumName)
        {
            var albums = this.unitOfWork.Albums.FindAll(a => a.Name == albumName);

            if (albums.Count() == 0)
            {
                throw new InvalidOperationException("No such album!");
            }
            else if (albums.Count() > 1)
            {
                throw new InvalidOperationException("More than 1 album with the same name!");
            }

            var album = albums.ToArray()[0];
            album.Tags.Add(tag);
            this.unitOfWork.Save();
        }

        public void ChangeAlbumProperty(int albumId, string albumProperty, string newValue)
        {
            var album = this.unitOfWork.Albums.FindById(albumId);

            if (album == null)
            {
                throw new ArgumentNullException("There is not an album with this id in the database!");
            }

            var albumPropertyType = typeof(Album)
                .GetProperty(albumProperty, BindingFlags.Instance | BindingFlags.Public);

            if (albumPropertyType == null)
            {
                throw new ArgumentException("There is no such property in Album class!");
            }

            try
            {
                albumPropertyType.SetValue(album, newValue);
                this.unitOfWork.Save();
            }
            catch (Exception)
            {
                throw new ArgumentException("The provided value is not suitable for this property type!");
            }
        }
    }
}

namespace PhotoShare.Data.Services
{
    using System;
    using Contracts;
    using System.Linq;
    using Models;
    using System.IO;

    public class PictureService
    {
        private IUnitOfWork unitOfWork;

        public PictureService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void UploadPicture(string albumName, string pictureTitle, string picturePath)
        {
            var albums = this.unitOfWork.Albums.FindAll(a => a.Name == albumName);

            if (albums.Count() == 0)
            {
                throw new InvalidOperationException("No such album!");
            }
            else if (albums.Count() > 1)
            {
                throw new InvalidOperationException("More than 1 album with this name!");
            }

            var album = albums.ToArray()[0];

            album.Pictures.Add(new Picture()
            {
                Title = pictureTitle,
                Path = picturePath
            });

            this.unitOfWork.Save();
        }

        public void UploadProfilePicture(string username, string picturePath)
        {
            var users = this.unitOfWork.Users.FindAll(u => u.Username == username);

            if (users.Count() == 0)
            {
                throw new InvalidOperationException("No such user!");
            }
            else if (users.Count() > 1)
            {
                throw new InvalidOperationException("More than 1 user with this username!");
            }

            var user = users.ToArray()[0];

            if (User.idOfLoggedUser != user.Id)
            {
                throw new InvalidOperationException("User " + username + " must be logged in to upload a profile picture!");
            }

            var albumIds = user.AlbumRoles
                .Where(ar => ar.Role == Role.Owner)
                .Select(ar => ar.Album.Id)
                .Distinct();

            if (albumIds.Count() == 0)
            {
                throw new InvalidOperationException("This user owns no albums!");
            }

            var picsByAlbum = this.unitOfWork.Albums
                .FindAll(a => albumIds.Contains(a.Id))
                .Select(a => a.Pictures)
                .Where(collOfPics => collOfPics.Any(p => p.Path == picturePath));

            if (picsByAlbum.Count() == 0)
            {
                throw new InvalidOperationException("No user album contains such picture!");
            }
            
            user.ProfilePicture = File.ReadAllBytes(picturePath);
            this.unitOfWork.Save();
        }
    }
}

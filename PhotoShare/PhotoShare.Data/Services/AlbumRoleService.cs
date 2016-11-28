namespace PhotoShare.Data.Services
{
    using Contracts;
    using Models;
    using System;
    using System.Linq;

    public class AlbumRoleService
    {
        private IUnitOfWork unitOfWork;

        public AlbumRoleService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void CreateAlbum(string username, string albumTitle, string colorStr, string[] tagNames)
        {
            var users = this.unitOfWork.Users.FindAll(u => u.Username == username);
            User user = null;

            if (users.Count() == 0)
            {
                throw new InvalidOperationException("No such user!");
            }
            else if (users.Count() > 1)
            {
                throw new InvalidOperationException("More than 1 user with the same name!");
            }

            user = users.ToArray()[0];

            if (User.idOfLoggedUser != user.Id)
            {
                throw new InvalidOperationException("User " + username + " must be logged in to create an album!");
            }

            Color color;

            try
            {
                color = (Color)Enum.Parse(typeof(Color), colorStr);
            }
            catch (Exception)
            {
                throw new ArgumentException("Invalid color!");
            }

            var album = new Album() { Name = albumTitle, BackgroundColor = color };

            if (tagNames != null)
            {
                for (int i = 0; i < tagNames.Length; i++)
                {
                    Tag tag = new Tag() { Name = tagNames[i] };
                    album.Tags.Add(tag);
                }
            }

            Role role = Role.Owner;

            var albumRole = new AlbumRole() { Album = album, User = user, Role = role };
            this.unitOfWork.AlbumRoles.Add(albumRole);
            this.unitOfWork.Save();
        }

        public void ShareAlbum(int albumId, string username, string permission)
        {
            var album = this.unitOfWork.Albums.FindById(albumId);

            if (album == null)
            {
                throw new ArgumentNullException("There is not an album with this id in the database!");
            }

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

            Role permissionRole;

            try
            {
                permissionRole = (Role)Enum.Parse(typeof(Role), permission);
            }
            catch (Exception)
            {
                throw new ArgumentException("This is not a valid permission type!");
            }

            var albumRoles = this.unitOfWork.AlbumRoles
                .FindAll(ar => ar.User.Username == username && ar.Album.Id == albumId);

            if (albumRoles.Count() == 0)
            {
                var albumRole = new AlbumRole()
                {
                    Album = album,
                    User = user,
                    Role = permissionRole
                };

                this.unitOfWork.AlbumRoles.Add(albumRole);
                this.unitOfWork.Save();
            }
            else if (albumRoles.Count() == 1)
            {
                var albumRole = albumRoles.ToArray()[0];

                if (albumRole.Role != permissionRole)
                {
                    albumRole.Role = permissionRole;
                    this.unitOfWork.Save();
                }
                else
                {
                    throw new InvalidOperationException("Permission role is already set to this value!");
                }
            }
            else
            {
                throw new InvalidOperationException("More than 1 AlbumRoles with these parameters!");
            }
        }
    }
}

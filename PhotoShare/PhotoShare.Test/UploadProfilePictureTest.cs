namespace PhotoShare.Test
{
    using Data.Contracts;
    using Data.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Setup;
    using System;
    using System.IO;

    [TestClass]
    public class UploadProfilePictureTest
    {
        private IUnitOfWork unitOfWork;
        private PictureService pictureService;

        [TestInitialize]
        public void Initialize()
        {
            this.unitOfWork = new CustomUnitOfWork();

            this.unitOfWork.Users.Add(new User()
            {
                Id = 1,
                FirstName = "Ivan",
                LastName = "Peshov",
                Age = 40,
                Username = "IvanP",
                Email = "abvbgbf@abv.bg",
                Password = "aLadasd?!23"
            });
            this.unitOfWork.Users.Add(new User()
            {
                Id = 2,
                FirstName = "Ivan",
                LastName = "Peshov",
                Age = 40,
                Username = "IvanP",
                Email = "abvbgbf@abv.bg",
                Password = "aLadasd?!23"
            });
            this.unitOfWork.Users.Add(new User()
            {
                Id = 3,
                FirstName = "Georgi",
                LastName = "Minchev",
                Age = 30,
                Username = "GeorgiM",
                Email = "sdfasfaf@google.com",
                Password = "1ewsdfFDS?!23"
            });
            this.unitOfWork.Users.Add(new User()
            {
                Id = 4,
                FirstName = "Profan",
                LastName = "Ivanov",
                Age = 34,
                Username = "ProfanI",
                Email = "sdfresfaf@abv.bg",
                Password = "da134sdfFdS?!23"
            });

            Album album = new Album()
            {
                Name = "Album One",
                IsPublic = true
            };

            Picture pic = new Picture()
            {
                Title = "Wow",
                Path = "specific_path"
            };

            album.Pictures.Add(pic);
            pic.Albums.Add(album);

            this.unitOfWork.Pictures.Add(pic);
            User user = this.unitOfWork.Users.FindById(4);

            AlbumRole albumRole = new AlbumRole()
            {
                User = user,
                Album = album,
                Role = Role.Owner
            };

            album.AlbumRoles.Add(albumRole);
            user.AlbumRoles.Add(albumRole);

            this.unitOfWork.Albums.Add(album);
            this.unitOfWork.AlbumRoles.Add(albumRole);

            this.pictureService = new PictureService(unitOfWork);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void ShouldThrowIfTheUserDoesntExist()
        {
            this.pictureService.UploadProfilePicture("alabala", "alabala");
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void ShouldThrowIfThereAreMoreThanOneUserWithSameUsername()
        {
            this.pictureService.UploadProfilePicture("IvanP", "alabala");
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void ShouldThrowIfUserIsNotLoggedIn()
        {
            this.pictureService.UploadProfilePicture("GeorgiM", "alabala");
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void ShouldThrowIfUserOwnsNoAlbums()
        {
            User.idOfLoggedUser = 3;
            this.pictureService.UploadProfilePicture("GeorgiM", "alabala");
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void ShouldThrowIfNoUserAlbumsContainPicWithPath()
        {
            User.idOfLoggedUser = 4;
            this.pictureService.UploadProfilePicture("ProfanI", "alabala");
        }

        [ExpectedException(typeof(FileNotFoundException))]
        [TestMethod]
        public void ShouldThrowFileNotFoundException()
        {
            User.idOfLoggedUser = 4;

            this.pictureService.UploadProfilePicture("ProfanI", "specific_path");
        }

        [TestCleanup]
        public void Cleanup()
        {
            User.idOfLoggedUser = null;
        }
    }
}

namespace PhotoShare.Test
{
    using Data.Contracts;
    using Data.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Setup;
    using System;

    [TestClass]
    public class MakeFriendsTest
    {
        private IUnitOfWork unitOfWork;
        private UserService userService;

        [TestInitialize]
        public void Initialize()
        {
            this.unitOfWork = new CustomUnitOfWork();
            this.unitOfWork.Users.Add(new User()
            {
                FirstName = "Ivan",
                LastName = "Peshov",
                Age = 40,
                Username = "IvanP",
                Email = "abvbgbf@abv.bg",
                Password = "aLadasd?!23"
            });
            this.unitOfWork.Users.Add(new User()
            {
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
            this.userService = new UserService(unitOfWork);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void ShouldThrowIfOneOfTheUsersDoesntExist()
        {
            this.userService.MakeFriends("ProfanI", "tosho");
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void ShouldThrowIfThereAreMoreThanOneUserWithSameUsername()
        {
            this.userService.MakeFriends("ProfanI", "IvanP");
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void ShouldThrowIfUserIsNotLoggedIn()
        {
            this.userService.MakeFriends("ProfanI", "GeorgiM");
        }
        
        [TestMethod]
        public void ShouldAddFriendToBothUsers()
        {
            User.idOfLoggedUser = 3;

            this.userService.MakeFriends("ProfanI", "GeorgiM");
            User user1 = this.unitOfWork.Users.FindById(3);
            User user2 = this.unitOfWork.Users.FindById(4);

            Assert.AreEqual(user1.Friends.Count, 1);
            Assert.AreEqual(user2.Friends.Count, 1);
        }

        [TestCleanup]
        public void Cleanup()
        {
            User.idOfLoggedUser = null;
        }
    }
}

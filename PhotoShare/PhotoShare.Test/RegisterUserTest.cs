namespace PhotoShare.Test
{
    using Data.Contracts;
    using Data.Services;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Models;
    using Setup;
    using System;
    using System.Linq;

    [TestClass]
    public class RegisterUserTest
    {
        private IUnitOfWork unitOfWork;
        private UserService userService;

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
            this.userService = new UserService(unitOfWork);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void ShouldThrowIfUserWithSameUsernameExists()
        {
            this.userService.RegisterUser("IvanP", "aaAA55??", "abvsdfs@abv.bg");
        }

        [TestMethod]
        public void ShouldRegisterUser()
        {
            this.userService.RegisterUser("ProfanI", "aaAA55??", "abvsdfs@abv.bg");
            var usersCount = this.unitOfWork.Users.FindAll(u => true).Count();
            Assert.AreEqual(usersCount, 2);
        }
    }
}

namespace PhotoShare.Data.Services
{
    using Contracts;
    using Models;
    using System;
    using System.Linq;
    using System.Reflection;

    public class UserService
    {
        private IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void DeleteUserByUsername(string username)
        {
            var users = this.unitOfWork.Users
                .FindAll(u => u.Username == username);

            if (users.Count() == 0)
            {
                throw new InvalidOperationException($"User with username {username} was not found");
            }
            else if (users.Count() > 1)
            {
                throw new InvalidOperationException($"More than 1 users with username {username} were found");
            }

            var user = users.ToArray()[0];

            if (User.idOfLoggedUser == user.Id)
            {
                throw new InvalidOperationException("User " + username + " cannot be deleted while logged in!");
            }

            user.IsDeleted = true;
            this.unitOfWork.Save();
        }

        public void Logout()
        {
            if (User.idOfLoggedUser == null)
            {
                throw new InvalidOperationException("No user is logged in");
            }

            User.idOfLoggedUser = null;
        }

        public void LoginUser(string username, string password)
        {
            if (User.idOfLoggedUser != null)
            {
                throw new InvalidOperationException("There is a logged in user already!");
            }

            var users = this.unitOfWork.Users
                .FindAll(u => u.Username == username);

            if (users.Count() == 0)
            {
                throw new InvalidOperationException("No such user!");
            }
            else if (users.Count() > 1)
            {
                throw new InvalidOperationException("More than 1 user with this username!");
            }

            var user = users.ToArray()[0];

            if (user.Password != password)
            {
                throw new ArgumentException("Password incorrect");
            }

            User.idOfLoggedUser = user.Id;
        }

        public void RegisterUser(string username, string password, string email)
        {
            var users = this.unitOfWork.Users
                .FindAll(u => u.Username == username);

            if (users.Count() == 0)
            {
                try
                {
                    User user = new User()
                    {
                        Username = username,
                        Password = password,
                        Email = email,
                        IsDeleted = false,
                        RegisteredOn = DateTime.Now,
                        LastTimeLoggedIn = DateTime.Now
                    };

                    this.unitOfWork.Users.Add(user);
                    this.unitOfWork.Save();
                }
                catch (Exception)
                {
                    throw new ArgumentException("Password or email were not valid!");
                }
            }
            else
            {
                throw new InvalidOperationException("Already registered user with this username!");
            }
        }

        public string GetFriendsOfUser(string username)
        {
            var users = this.unitOfWork.Users
                .FindAll(u => u.Username == username);

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
                throw new InvalidOperationException("User " + username + " must be logged in to get information about their friends!");
            }

            var userFriendsNames = user.Friends.Select(f => f.FullName);

            return string.Join(Environment.NewLine, userFriendsNames);
        }

        public void MakeFriends(string username1, string username2)
        {
            var users = this.unitOfWork.Users
                .FindAll(u => u.Username == username1 || u.Username == username2);

            if (users.Count() < 2)
            {
                throw new InvalidOperationException("There are not 2 users with these usernames!");
            }
            else if (users.Count() > 2)
            {
                throw new InvalidOperationException("There are more than 2 users with these usernames!");
            }

            var user1 = users.ToArray()[0];
            var user2 = users.ToArray()[1];

            if (User.idOfLoggedUser != user1.Id)
            {
                throw new InvalidOperationException("User " + username1 + " must be logged in to make friends with " + username2 + "!");
            }

            user1.Friends.Add(user2);
            user2.Friends.Add(user1);
            this.unitOfWork.Save();
        }

        public void ChangeUserProperty(string username, string userProperty, string newValue)
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
                throw new InvalidOperationException("User " + username + " must be logged in to make changes in their profile!");
            }

            var userPropertyType = typeof(User)
                .GetProperty(userProperty, BindingFlags.Instance | BindingFlags.Public);

            if (userPropertyType == null)
            {
                throw new ArgumentException("There is no such property in Album class!");
            }
            else if (userPropertyType == typeof(User).GetProperty("Username"))
            {
                throw new InvalidOperationException("Can not change username!");
            }

            try
            {
                userPropertyType.SetValue(user, newValue);
                this.unitOfWork.Save();
            }
            catch (Exception)
            {
                throw new ArgumentException("The provided value is not suitable for this property type!");
            }
        }
    }
}

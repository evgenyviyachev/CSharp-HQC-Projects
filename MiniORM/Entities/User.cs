namespace MiniORM.Entities
{
    using Attributes;
    using System;

    [Entity(TableName = "Users")]
    public class User
    {
        [Id]
        private int id;

        [Column(Name = "Username")]
        private string username;

        [Column(Name = "Password")]
        private string password;

        [Column(Name = "Age")]
        private int age;

        [Column(Name = "RegistrationDate")]
        private DateTime registrationDate;

        [Column(Name = "LastLoginTime")]
        private DateTime lastLoginTime;

        [Column(Name = "IsActive")]
        private bool isActive;

        public User(string username, string password, int age, DateTime registrationDate, DateTime lastLoginTime, bool isActive)
        {
            this.Username = username;
            this.Password = password;
            this.Age = age;
            this.RegistrationDate = registrationDate;
            this.LastLoginTime = lastLoginTime;
            this.IsActive = isActive;
        }

        public DateTime RegistrationDate
        {
            get
            {
                return registrationDate;
            }

            set
            {
                registrationDate = value;
            }
        }

        public int Age
        {
            get
            {
                return age;
            }

            set
            {
                age = value;
            }
        }

        public string Username
        {
            get
            {
                return username;
            }

            set
            {
                username = value;
            }
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
            }
        }

        public DateTime LastLoginTime
        {
            get
            {
                return lastLoginTime;
            }

            set
            {
                lastLoginTime = value;
            }
        }

        public bool IsActive
        {
            get
            {
                return isActive;
            }

            set
            {
                isActive = value;
            }
        }
    }
}

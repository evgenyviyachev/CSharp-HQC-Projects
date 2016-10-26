namespace MiniORM.TestClasses
{
    using Entities;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    //Task 16
    public class UserActiveChecker
    {
        private IDBContext dbContext;

        public UserActiveChecker(IDBContext dbContext)
        {
            this.DbContext = dbContext;
        }

        public IDBContext DbContext
        {
            get
            {
                return dbContext;
            }

            set
            {
                dbContext = value;
            }
        }

        public void DeleteInactiveUser(string username)
        {
            User user = this.dbContext.FindFirst<User>($"WHERE Username = '{username}'");

            var timeDiff = DateTime.Now - user.LastLoginTime;
            var timeDiffDays = timeDiff.TotalDays;

            if (timeDiffDays > 365)
            {
                Console.WriteLine($"User {username} was last online more than a year.");
                Console.WriteLine("Would you like to delete that user? (yes/no)");
                string answer = Console.ReadLine();

                if (answer == "yes")
                {
                    this.dbContext.DeleteFrom<User>($"WHERE Username = '{username}'");
                    Console.WriteLine($"User {username} was successfully deleted from the database");
                }
                else if (answer == "no")
                {
                    Console.WriteLine($"User {username} was not deleted from the database");
                }
                else
                {
                    throw new ArgumentException("You chose an invalid option!");
                }
            }
            else
            {
                string timeAway = "";

                if (timeDiff.TotalSeconds < 1)
                {
                    timeAway = "less than a second";
                }
                else if (timeDiff.TotalMinutes < 1)
                {
                    timeAway = "less than a minute";
                }
                else if (timeDiff.TotalHours < 1)
                {
                    timeAway = $"{timeDiff.TotalMinutes} minutes ago";
                }
                else if (timeDiff.TotalDays < 1)
                {
                    timeAway = $"{timeDiff.TotalHours} hours ago";
                }
                else if ((timeDiff.TotalDays / 30) < 1)
                {
                    timeAway = $"{timeDiff.TotalDays} days ago";
                }
                else
                {
                    timeAway = $"{timeDiff.TotalDays / 30} months ago";
                }

                Console.WriteLine($"User {username} was last online {timeAway}");
            }
        }
    }
}

namespace MiniORM
{
    using Core;
    using Entities;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using TestClasses;

    public class Program
    {
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            ConnectionStringBuilder builder = new ConnectionStringBuilder("MiniORMDB");
            IDBContext dbContext = new EntityManager(builder.ConnectionString, true);

            //-----------------------------------------------------------------
            //Task 11
            //var users = dbContext.FindAll<User>("WHERE RegistrationDate > '2010' AND Age >= 18");

            //foreach (var user in users)
            //{
            //    Console.WriteLine(user.Username + " " + user.Password);
            //}
            //-----------------------------------------------------------------
            ///Task 12
            //BookTitleTrimmer trimmer = new BookTitleTrimmer(dbContext);
            //trimmer.Trim(5);
            //-----------------------------------------------------------------
            //Task 13
            //var books = dbContext.FindAll<Book>("ORDER BY Rating DESC, Title ASC").ToList();

            //for (int i = 0; i < 3; i++)
            //{
            //    Console.WriteLine($"{books[i].Title} ({books[i].Author}) – {books[i].Rating}/10");
            //}
            //-----------------------------------------------------------------
            //Task 14
            //BookTitleUpdater updater = new BookTitleUpdater(dbContext);
            //updater.Update(1960);
            //-----------------------------------------------------------------
            //Task 15
            //int deletedBooks = dbContext.DeleteFrom<Book>("WHERE Rating < 8");
            //Console.WriteLine($"{deletedBooks} books has been deleted from the database");
            //-----------------------------------------------------------------
            //Task 16
            //UserActiveChecker checker = new UserActiveChecker(dbContext);
            //checker.DeleteInactiveUser("Pesho");
        }
    }
}

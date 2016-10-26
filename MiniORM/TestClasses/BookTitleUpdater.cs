namespace MiniORM.TestClasses
{
    using System;
    using Entities;
    using Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    //Task 14
    public class BookTitleUpdater
    {
        private IDBContext dbContext;

        public BookTitleUpdater(IDBContext dbContext)
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

        public void Update(int year)
        {
            var allBooks = this.dbContext.FindAll<Book>();
            List<string> changedBooksTitles = new List<string>();
            int counter = 0;

            foreach (var book in allBooks)
            {
                if (book.PublishedOn.Year > year && book.IsHardCovered)
                {
                    book.Title = book.Title.ToUpper();
                    this.dbContext.Persist(book);
                    counter++;
                    changedBooksTitles.Add(book.Title);
                }
            }

            Console.WriteLine($"Books released after {year} year: {counter}");
            changedBooksTitles = changedBooksTitles.OrderBy(x => x).ToList();
            Console.WriteLine(string.Join("\n", changedBooksTitles));
        }
    }
}

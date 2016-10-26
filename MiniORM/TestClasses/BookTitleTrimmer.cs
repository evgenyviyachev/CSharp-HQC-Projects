namespace MiniORM.TestClasses
{
    using System;
    using Entities;
    using Interfaces;

    //Task 12
    public class BookTitleTrimmer
    {
        private IDBContext dbContext;

        public BookTitleTrimmer(IDBContext dbContext)
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

        public void Trim(int maxLength)
        {
            var allBooks = this.dbContext.FindAll<Book>();
            int counter = 0;

            foreach (var book in allBooks)
            {
                if (book.Title.Length > maxLength && book.IsHardCovered)
                {
                    book.Title = book.Title.Substring(0, maxLength);                    
                    this.dbContext.Persist(book);
                    counter++;
                }
            }

            Console.WriteLine($"{counter} books now have title with length of {maxLength}");
        }
    }
}

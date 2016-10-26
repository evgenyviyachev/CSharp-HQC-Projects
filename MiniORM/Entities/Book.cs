namespace MiniORM.Entities
{
    using MiniORM.Attributes;
    using System;

    [Entity(TableName = "Books")]
    public class Book
    {
        [Id]
        private int id;

        [Column(Name = "Title")]
        private string title;

        [Column(Name = "Author")]
        private string author;

        [Column(Name = "PublishedOn")]
        private DateTime publishedOn;

        [Column(Name = "Language")]
        private string language;

        [Column(Name = "IsHardCovered")]
        private bool isHardCovered;

        [Column(Name = "Rating")]
        private double rating;

        public Book(string title, string author, DateTime publishedOn, string language, bool isHardCovered, double rating)
        {
            this.Title = title;
            this.Author = author;
            this.PublishedOn = publishedOn;
            this.Language = language;
            this.IsHardCovered = isHardCovered;
            this.Rating = rating;
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

        public string Author
        {
            get
            {
                return author;
            }

            set
            {
                author = value;
            }
        }

        public DateTime PublishedOn
        {
            get
            {
                return publishedOn;
            }

            set
            {
                publishedOn = value;
            }
        }

        public string Language
        {
            get
            {
                return language;
            }

            set
            {
                language = value;
            }
        }

        public bool IsHardCovered
        {
            get
            {
                return isHardCovered;
            }

            set
            {
                isHardCovered = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        public double Rating
        {
            get
            {
                return rating;
            }

            set
            {
                rating = value;
            }
        }
    }
}

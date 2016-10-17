namespace CS_OOP_Advanced_Exam_Prep_July_2016.Models.Products
{
    using System;
    using CS_OOP_Advanced_Exam_Prep_July_2016.Contracts;

    public abstract class Product : IProduct
    {
        private string name;
        private int size;
        private int id;

        public Product(string name, int size)
        {
            this.Name = name;
            this.Size = size;
        }

        public int ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public virtual int Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
            }
        }

        public override string ToString()
        {
            return $"{this.GetType().Name}: {this.ID}. Size: {this.Size}. Name: {this.Name}”";
        }
    }
}

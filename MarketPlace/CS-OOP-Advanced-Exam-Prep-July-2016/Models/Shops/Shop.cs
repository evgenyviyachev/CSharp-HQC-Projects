namespace CS_OOP_Advanced_Exam_Prep_July_2016.Models.Shops
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CS_OOP_Advanced_Exam_Prep_July_2016.Contracts;

    public abstract class Shop : IShop
    {
        private int capacity;
        private IShop successor;
        private ICollection<IProduct> products;

        public Shop(int capacity, IShop successor)
        {
            this.Capacity = capacity;
            this.successor = successor;
            this.products = new List<IProduct>();
        }

        public int Capacity
        {
            get
            {
                return this.capacity;
            }
            set
            {
                this.capacity = value;
            }
        }

        public IShop Successor
        {
            get
            {
                return this.successor;
            }
        }

        public string AddProduct(IProduct product)
        {
            if (this.Capacity < this.products.Sum(x => x.Size) + product.Size)
            {
                return this.successor.AddProduct(product);
            }
            else
            {
                this.products.Add(product);
                return $"Product {product.ID} moved to market {this.GetType().Name}";
            }
        }

        public bool IsEmpty()
        {
            return this.products.Count == 0;
        }

        public ICollection<IProduct> Products
        {
            get
            {
                return this.products;
            }
        }
    }
}

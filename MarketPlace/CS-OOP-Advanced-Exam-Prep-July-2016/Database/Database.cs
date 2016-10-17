using CS_OOP_Advanced_Exam_Prep_July_2016.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CS_OOP_Advanced_Exam_Prep_July_2016.Database
{
    public class Database : IDatabase
    {
        private IList<IShop> shops;
        private IList<IProduct> products;

        public Database()
        {
            this.shops = new List<IShop>();
            this.products = new List<IProduct>();
        }

        public int NextId
        {
            get
            {
                return this.products.Count + 1;
            }
        }

        public string AddProduct(IProduct product)
        {
            this.products.Add(product);
            product.ID = this.NextId;

            return $"Product {product.ID} registered successfully";
        }

        public string GetProductsBySizeNameType(int size, string name, string type)
        {
            var foundProducts = products.Where(p => p.Name == name && p.Size == size && p.GetType().Name == type);

            if (foundProducts == null)
            {
                throw new ArgumentNullException("No products by the given criteria");
            }

            return string.Join(Environment.NewLine, foundProducts);
        }

        public string GetProductsBySizeName(int size, string name)
        {
            var foundProducts = products.Where(p => p.Name == name && p.Size == size);

            if (foundProducts == null)
            {
                throw new ArgumentNullException("No products by the given criteria");
            }

            return string.Join(Environment.NewLine, foundProducts);
        }

        public string GetProductByID(int id)
        {
            if (!this.products.Any(p => p.ID == id))
            {
                throw new ArgumentNullException($"Product {id} does not exist");
            }

            IProduct product = products.First(p => p.ID == id);

            return $"{product.GetType().Name}: {product.ID}. Size: {product.Size}. Name: {product.Name}";
        }

        public string EditProduct(int id, string newName, int newSize)
        {
            if (!this.products.Any(p => p.ID == id))
            {
                throw new ArgumentNullException($"Product {id} does not exist");
            }

            IProduct product = products.First(p => p.ID == id);
            product.Name = newName;
            product.Size = newSize;

            return $"Product {id} successfully edited";
        }

        public string AddProductToShop(string typeOfShop, int id)
        {
            if (!this.products.Any(p => p.ID == id))
            {
                throw new ArgumentNullException($"Product {id} does not exist");
            }

            IProduct product = this.products.First(p => p.ID == id);

            if (this.shops.Any(sh => sh.Products.Any(p => p.ID == id)))
            {
                IShop shopContain = this.shops.First(sh => sh.Products.Any(p => p.ID == id));

                return $"Product {id} is already registered to a market {shopContain.GetType().Name}";
            }

            IShop shop = this.shops.First(s => s.GetType().Name == typeOfShop);

            return shop.AddProduct(product);
        }

        public string GetProductsFromShop(string typeOfShop)
        {
            IShop shop = this.shops.First(s => s.GetType().Name == typeOfShop);

            if (shop.IsEmpty())
            {
                throw new ArgumentNullException("No products by the given criteria");
            }

            return string.Join(Environment.NewLine, shop.Products);
        }
    }
}

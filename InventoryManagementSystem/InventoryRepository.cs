using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    internal class InventoryRepository : IInventoryRepository
    {
        private readonly Inventory _inventory;
        public InventoryRepository()
        {
            _inventory = new Inventory();
        }
        public void AddProduct(Product product)
        {
            _inventory.AddProduct(product);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _inventory.Products.AsEnumerable<Product>();
        }
        public IEnumerable<Product> GetProductByName(string productName)
        {
            var result = from s in _inventory.Products
                         where s.Name == productName
                         select s;
            return result;
        }
        public void EditProduct(Product oldProduct, Product newProduct)
        {
            oldProduct.Name = newProduct.Name;
            oldProduct.Price = newProduct.Price;
            oldProduct.Quantity = newProduct.Quantity;
        }
    }
}

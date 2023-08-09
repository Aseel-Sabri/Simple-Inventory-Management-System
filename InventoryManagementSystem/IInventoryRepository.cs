using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    internal interface IInventoryRepository
    {
        void AddProduct(Product product);
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductByName(string productName);
        void EditProduct(Product oldProduct, Product newProduct);
    }
}

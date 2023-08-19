using FluentResults;
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
        Result AddProduct(Product product);
        IEnumerable<Product> GetAllProducts();
        Result<Product> GetProductByName(string productName);
        Result EditProduct(string productName, Product newProduct);
        Result DeleteProduct(string productName);
        public bool HasProduct(string productName);
    }
}
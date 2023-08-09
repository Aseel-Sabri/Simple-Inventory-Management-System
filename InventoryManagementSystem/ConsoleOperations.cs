using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    internal class ConsoleOperations
    {
        private readonly IInventoryServices _inventorySrevices;
        public ConsoleOperations()
        {
            _inventorySrevices = new InventoryServices();
        }
        public void AddProduct()
        {
            string name = GetProductName();
            double price = double.Parse(GetProductPrice());
            int quantity = int.Parse(GetProductQuantity());

            Product product = new()
            {
                Name = name,
                Price = price,
                Quantity = quantity
            };

            _inventorySrevices.AddProduct(product);
        }

        public void GetAllProducts()
        {
            var products = _inventorySrevices.GetAllProducts();
            if (!products.Any())
            {
                Console.WriteLine("No products available");
                return;
            }
            foreach (Product product in products)
            {
                Console.WriteLine(product);
            }
        }

        public void SearchProduct()
        {
            string name = GetProductName();
            GetProductByName(name);
        }

        public bool GetProductByName(string name)
        {
            Console.WriteLine();
            var products = _inventorySrevices.GetProductByName(name);
            if (!products.Any())
            {
                Console.WriteLine("Product Not Found");
                return false;
            }
            foreach (Product product in products)
            {
                Console.WriteLine(product);
            }
            return true;
        }

        public void EditProduct()
        {
            string name = GetProductName();
            if (!GetProductByName(name))
            {
                return;
            }

            Console.WriteLine();
            string newName = GetProductName();
            double newPrice = double.Parse(GetProductPrice());
            int newQuantity = int.Parse(GetProductQuantity());

            Product newProduct = new()
            {
                Name = newName,
                Price = newPrice,
                Quantity = newQuantity
            };

            var products = _inventorySrevices.GetProductByName(name);
            foreach (Product product in products)
            {
                _inventorySrevices.EditProduct(product, newProduct);
            }

            Console.WriteLine("\nEdited Successfully");
        }

        static string GetProductName()
        {
            return GetProductField(ProductValidation.ValidateProductName, "Name");
        }

        static string GetProductPrice()
        {
            return GetProductField(ProductValidation.ValidateProductPrice, "Price");
        }

        static string GetProductQuantity()
        {
            return GetProductField(ProductValidation.ValidateProductQuantity, "Quantity");
        }


        static string GetProductField(Func<string?, bool> fieldValidationMethod, string fieldName)
        {
            string? value = string.Empty;
            bool valid = false;
            while (!valid)
            {
                Console.Write($"Product {fieldName}: ");
                value = Console.ReadLine();
                valid = fieldValidationMethod(value);
                if (!valid)
                {
                    Console.WriteLine($"Invalid Product {fieldName}");
                }
            }
            return value ?? string.Empty;
        }
    }
}

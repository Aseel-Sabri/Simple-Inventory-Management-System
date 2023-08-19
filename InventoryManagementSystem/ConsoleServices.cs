using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    internal class ConsoleServices
    {
        private readonly IInventoryRepository _inventoryRepository;

        public ConsoleServices()
        {
            _inventoryRepository = new InventoryRepository();
        }

        public void AddProduct()
        {
            string name = GetProductName();
            double price = GetProductPrice();
            int quantity = GetProductQuantity();
            Console.WriteLine();

            Product product = new()
            {
                Name = name,
                Price = price,
                Quantity = quantity
            };

            var addResult = _inventoryRepository.AddProduct(product);
            if (addResult.IsFailed)
            {
                Console.WriteLine(addResult.Errors.First().Message);
                return;
            }

            Console.WriteLine("Added Successfully");
        }

        public void DisplayAllProducts()
        {
            var products = _inventoryRepository.GetAllProducts().ToList();
            if (!products.Any())
            {
                Console.WriteLine("No Products Available");
                return;
            }

            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }

        public void SearchProduct()
        {
            string name = GetProductName();
            DisplayProductByName(name);
        }

        private void DisplayProductByName(string name)
        {
            Console.WriteLine();
            var productResult = _inventoryRepository.GetProductByName(name);
            if (productResult.IsFailed)
            {
                Console.WriteLine(productResult.Errors.First().Message);
                return;
            }

            Console.WriteLine(productResult.Value);
        }

        public void EditProduct()
        {
            Console.WriteLine("Name of Product to Edit:");
            string productName = GetProductName();
            Console.WriteLine();

            Console.WriteLine("New Product Values:");
            string newName = GetProductName();
            double newPrice = GetProductPrice();
            int newQuantity = GetProductQuantity();
            Console.WriteLine();

            Product newProduct = new()
            {
                Name = newName,
                Price = newPrice,
                Quantity = newQuantity
            };

            var editResult = _inventoryRepository.EditProduct(productName, newProduct);
            if (editResult.IsFailed)
            {
                Console.WriteLine(editResult.Errors.First().Message);
                return;
            }

            Console.WriteLine("\nEdited Successfully");
        }

        public void DeleteProduct()
        {
            Console.WriteLine("Name of Product to Delete:");
            string productName = GetProductName();
            Console.WriteLine();

            var deleteResult = _inventoryRepository.DeleteProduct(productName);
            if (deleteResult.IsFailed)
            {
                Console.WriteLine(deleteResult.Errors.First().Message);
                return;
            }

            Console.WriteLine("\nDeleted Successfully");
        }

        private static string GetProductName()
        {
            return GetProductField(ProductValidation.ValidateProductName, "Name");
        }

        private static double GetProductPrice()
        {
            return double.Parse(GetProductField(ProductValidation.ValidateProductPrice, "Price"));
        }

        private static int GetProductQuantity()
        {
            return int.Parse(GetProductField(ProductValidation.ValidateProductQuantity, "Quantity"));
        }


        private static string GetProductField(Func<string?, bool> fieldValidationMethod, string fieldName)
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
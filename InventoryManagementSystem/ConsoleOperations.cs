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

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementSystem
{
    internal class ConsoleUI : IUserInterface
    {
        private readonly ConsoleServices _consoleServices;
        public ConsoleUI()
        {
            _consoleServices = new ConsoleServices();
        }
        enum Operation
        {
            None = 0,
            AddProduct = 1,
            ViewAllProducts = 2,
            EditProduct = 3,
            DeleteProduct = 4,
            SearchProduct = 5,
            Exit = 6
        }

        public void Display()
        {
            DisplayMenu();
        }

        void DisplayMenu()
        {
            Operation op;
            do
            {
                DisplayOptions();
                op = ReadUserOption();
                PerformOperation(op);
            } while (op != Operation.Exit);
        }

        void DisplayOptions()
        {
            Console.WriteLine("\nChoose Operation\n");
            Console.WriteLine("1. Add a product");
            Console.WriteLine("2. View all products");
            Console.WriteLine("3. Edit a product");
            Console.WriteLine("4. Delete a product");
            Console.WriteLine("5. Search for a product");
            Console.WriteLine("6. Exit");
        }

        Operation ReadUserOption()
        {
            string? input = Console.ReadLine();
            Console.WriteLine();
            if (!string.IsNullOrEmpty(input))
            {
                if (int.TryParse(input, out int op) && op > (int)Operation.None && op <= (int)Operation.Exit)
                {
                    return (Operation)op;
                }
            }
            Console.WriteLine($"Operation must be an integer within the range 1 - {(int)Operation.Exit}");
            return Operation.None;
        }

        void PerformOperation(Operation op)
        {
            switch (op)
            {
                case Operation.AddProduct:
                    {
                        _consoleServices.AddProduct();
                        return;
                    }
                case Operation.ViewAllProducts:
                    {
                        _consoleServices.GetAllProducts();
                        return;
                    }
                case Operation.EditProduct:
                    {
                        _consoleServices.EditProduct();
                        return;
                    }
                case Operation.DeleteProduct:
                    {
                        _consoleServices.DeleteProduct();
                        return;
                    }
                case Operation.SearchProduct:
                    {
                        _consoleServices.SearchProduct();
                        return;
                    }
            }
        }
    }
}

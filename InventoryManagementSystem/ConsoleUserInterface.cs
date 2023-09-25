using FluentResults;

namespace InventoryManagementSystem
{
    public class ConsoleUserInterface : IUserInterface
    {
        private readonly IProductServices _productServices;

        public ConsoleUserInterface(IProductServices productServices)
        {
            _productServices = productServices;
        }


        enum Operation
        {
            AddProduct = 1,
            ViewAllProducts = 2,
            EditProduct = 3,
            DeleteProduct = 4,
            SearchProduct = 5,
            Exit = 6
        }

        public void Run()
        {
            DisplayMenu();
        }

        void DisplayMenu()
        {
            Result<Operation> opResult;
            do
            {
                DisplayOptions();
                opResult = ReadUserOption();
                if (opResult.IsFailed)
                {
                    Console.WriteLine(opResult.Errors.First().Message);
                    continue;
                }

                PerformOperation(opResult.ValueOrDefault);
            } while (opResult.ValueOrDefault != Operation.Exit);
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

        Result<Operation> ReadUserOption()
        {
            string? input = Console.ReadLine();
            Console.WriteLine();
            if (IsValidOperation(input))
            {
                Operation.TryParse(input, out Operation op);
                return Result.Ok(op);
            }

            return Result.Fail($"Operation must be an integer within the range 1 - {(int)Operation.Exit}");

            #region local function

            bool IsValidOperation(string? input)
            {
                return !string.IsNullOrEmpty(input)
                       && Operation.TryParse(input, out Operation op)
                       && Operation.IsDefined(op);
            }

            #endregion
        }

        void PerformOperation(Operation op)
        {
            switch (op)
            {
                case Operation.AddProduct:
                {
                    _productServices.AddProduct();
                    return;
                }
                case Operation.ViewAllProducts:
                {
                    _productServices.DisplayAllProducts();
                    return;
                }
                case Operation.EditProduct:
                {
                    _productServices.EditProduct();
                    return;
                }
                case Operation.DeleteProduct:
                {
                    _productServices.DeleteProduct();
                    return;
                }
                case Operation.SearchProduct:
                {
                    _productServices.SearchProduct();
                    return;
                }
            }
        }
    }
}
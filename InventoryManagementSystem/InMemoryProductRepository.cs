using FluentResults;

namespace InventoryManagementSystem
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new();

        public Result AddProduct(Product product)
        {
            if (HasProductWithName(product.Name))
            {
                return Result.Fail($"Could Not Add Product, Product with Name '{product.Name}' Already Exists");
            }

            _products.Add(product);
            return Result.Ok();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _products.AsEnumerable<Product>();
        }

        public Result<Product> GetProductByName(string productName)
        {
            var result = (from s in _products
                where s.Name == productName
                select s).ToList();
            return result.Any()
                ? Result.Ok(result.First())
                : Result.Fail($"No Product With Name '{productName}' Exists");
        }

        public Result EditProduct(string productName, Product newProduct)
        {
            var productResult = GetProductByName(productName);
            if (productResult.IsFailed)
            {
                return Result.Fail($"Could Not Edit The Product, No Product With Name '{productName}' Exists");
            }

            productResult.Value.Name = newProduct.Name;
            productResult.Value.Price = newProduct.Price;
            productResult.Value.Quantity = newProduct.Quantity;
            return Result.Ok();
        }

        public Result DeleteProduct(string productName)
        {
            return Result.OkIf(_products.RemoveAll(product => product.Name == productName) > 0,
                $"Could Not Remove The Product, No Product With Name '{productName}' Exists");
        }

        public bool HasProductWithName(string productName)
        {
            return _products.Any(product => product.Name == productName);
        }
    }
}
using FluentResults;

namespace InventoryManagementSystem
{
    public interface IProductRepository
    {
        Result AddProduct(Product product);
        IEnumerable<Product> GetAllProducts();
        Result<Product> GetProductByName(string productName);
        Result EditProduct(string productName, Product newProduct);
        Result DeleteProduct(string productName);
        public bool HasProduct(string productName);
    }
}
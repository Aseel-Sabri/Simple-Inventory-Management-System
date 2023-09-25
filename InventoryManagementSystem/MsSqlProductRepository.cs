using System.Data;
using System.Data.SqlClient;
using FluentResults;

namespace InventoryManagementSystem
{
    public class MsSqlProductRepository : IProductRepository
    {
        private const string DatabaseType = "MS SQL";

        private readonly string? _connectionString = AppConfig.GetConnectionString(DatabaseType);

        public Result AddProduct(Product product)
        {
            if (HasProductWithName(product.Name))
            {
                return Result.Fail($"Could Not Add Product, Product with Name '{product.Name}' Already Exists");
            }

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var query = "INSERT INTO Products VALUES (@productName, @productPrice, @productQuantity)";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@productName", product.Name);
            command.Parameters.AddWithValue("@productPrice", product.Price);
            command.Parameters.AddWithValue("@productQuantity", product.Quantity);

            var rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0
                ? Result.Ok()
                : Result.Fail($"Could Not Add Product");
        }

        public IEnumerable<Product> GetAllProducts()
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var query = "SELECT * FROM Products";
            using var command = new SqlCommand(query, connection);


            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                // Should I return it as list instead of IEnumerable?
                yield return MapProduct(reader);
            }
        }

        public Result<Product> GetProductByName(string productName)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var query = "SELECT * FROM Products WHERE [Name] = @productName";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@productName", productName);


            using var reader = command.ExecuteReader();
            if (!reader.Read())
            {
                return Result.Fail($"No Product With Name '{productName}' Exists");
            }

            var product = MapProduct(reader);
            return Result.Ok(product);
        }


        public Result EditProduct(string productName, Product newProduct)
        {
            if (HasProductWithName(newProduct.Name))
            {
                return Result.Fail($"Could Not Edit The Product, Product with Name '{newProduct.Name}' Already Exists");
            }

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var query = @"UPDATE Products
                            SET [Name] = @newProductName, Price = @newProductPrice, Quantity = @newProductQuantity
                            WHERE [Name] = @productName";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@newProductName", newProduct.Name);
            command.Parameters.AddWithValue("@newProductPrice", newProduct.Price);
            command.Parameters.AddWithValue("@newProductQuantity", newProduct.Quantity);
            command.Parameters.AddWithValue("@productName", productName);
            var rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0
                ? Result.Ok()
                : Result.Fail($"Could Not Edit The Product, No Product With Name '{productName}' Exists");
        }

        public Result DeleteProduct(string productName)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var query = @"DELETE FROM Products WHERE [Name] = @productName";
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@productName", productName);
            var rowsAffected = command.ExecuteNonQuery();

            return rowsAffected > 0
                ? Result.Ok()
                : Result.Fail($"Could Not Remove The Product, No Product With Name '{productName}' Exists");
        }

        public bool HasProductWithName(string productName)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            var query = "SELECT [Id] FROM Products WHERE [Name] = @productName";
            using var selectCommand = new SqlCommand(query, connection);

            selectCommand.Parameters.AddWithValue("@productName", productName);
            var selectResult = selectCommand.ExecuteScalar();
            return selectResult is not null;
        }

        private Product MapProduct(IDataRecord reader)
        {
            var product = new Product()
            {
                Id = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("ID"))),
                Name = reader.GetValue(reader.GetOrdinal("Name")).ToString(),
                Price = Convert.ToDouble(reader.GetValue(reader.GetOrdinal("Price"))),
                Quantity = Convert.ToInt32(reader.GetValue(reader.GetOrdinal("Quantity")))
            };
            return product;
        }
    }
}
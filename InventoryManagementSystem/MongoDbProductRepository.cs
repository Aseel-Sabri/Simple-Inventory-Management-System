using FluentResults;
using MongoDB.Driver;
using MongoDB.Bson;

namespace InventoryManagementSystem;

public class MongoDbProductRepository : IProductRepository
{
    private const string ProductsCollection = "Products";
    private const string NameAttribute = "Name";
    private const string PriceAttribute = "Price";
    private const string QuantityAttribute = "Quantity";

    private readonly IMongoCollection<Product> _collection;

    public MongoDbProductRepository()
    {
        var connectionString = AppConfig.GetConnectionString();
        var databaseName = AppConfig.GetDatabaseName();
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<Product>(ProductsCollection);
    }

    public Result AddProduct(Product product)
    {
        if (HasProductWithName(product.Name))
        {
            return Result.Fail($"Could Not Add Product, Product with Name '{product.Name}' Already Exists");
        }

        product.Id = ObjectId.GenerateNewId();
        _collection.InsertOne(product);
        return Result.Ok();
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _collection.Find(new BsonDocument()).ToList();
    }

    public Result<Product> GetProductByName(string productName)
    {
        var filter = Builders<Product>.Filter.Eq(NameAttribute, productName);
        var product = _collection.Find(filter).FirstOrDefault();
        return product is null ? Result.Fail($"No Product With Name '{productName}' Exists") : Result.Ok(product);
    }

    public Result EditProduct(string productName, Product newProduct)
    {
        if (HasProductWithName(newProduct.Name))
        {
            return Result.Fail($"Could Not Edit The Product, Product with Name '{newProduct.Name}' Already Exists");
        }

        var filter = Builders<Product>.Filter.Eq(NameAttribute, productName);
        var update = Builders<Product>.Update
            .Set(NameAttribute, newProduct.Name)
            .Set(PriceAttribute, newProduct.Price)
            .Set(QuantityAttribute, newProduct.Quantity);
        var updateResult = _collection.UpdateOne(filter, update);
        return updateResult.ModifiedCount > 0
            ? Result.Ok()
            : Result.Fail($"Could Not Edit The Product, No Product With Name '{productName}' Exists");
    }

    public Result DeleteProduct(string productName)
    {
        var filter = Builders<Product>.Filter.Eq(NameAttribute, productName);
        var deleteResult = _collection.DeleteOne(filter);
        return deleteResult.DeletedCount == 1
            ? Result.Ok()
            : Result.Fail($"Could Not Remove The Product, No Product With Name '{productName}' Exists");
    }

    public bool HasProductWithName(string productName)
    {
        var filter = Builders<Product>.Filter.Eq(NameAttribute, productName);
        return _collection.CountDocuments(filter) > 0;
    }
}
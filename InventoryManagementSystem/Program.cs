namespace InventoryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // IProductRepository productRepository = new MsSqlProductRepository();
            IProductRepository productRepository = new MongoDbProductRepository();
            IProductServices productServices = new ProductServices(productRepository);
            IUserInterface userInterface = new ConsoleUserInterface(productServices);
            userInterface.Run();
        }
    }
}
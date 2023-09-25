using System.Data.SqlClient;

namespace InventoryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var databaseType = AppConfig.GetDatabaseType();
            IProductRepository productRepository = null;
            switch (databaseType)
            {
                case "MS SQL":
                {
                    SetupMsSqlDatabase();
                    productRepository = new MsSqlProductRepository();
                    break;
                }
                case "MongoDB":
                {
                    productRepository = new MongoDbProductRepository();
                    break;
                }
            }

            IProductServices productServices = new ProductServices(productRepository);
            IUserInterface userInterface = new ConsoleUserInterface(productServices);
            userInterface.Run();
        }

        private static void SetupMsSqlDatabase()
        {
            var conStringBuilder = new SqlConnectionStringBuilder(AppConfig.GetConnectionString());
            var databaseName = conStringBuilder.InitialCatalog;
            conStringBuilder.InitialCatalog = "master";

            using (var connection = new SqlConnection(conStringBuilder.ToString()))
            {
                connection.Open();
                var commandStr = @$"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = '{databaseName}')
                                        BEGIN
                                            CREATE DATABASE {databaseName};
                                        END;";
                using (var command = new SqlCommand(commandStr, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            using (var connection = new SqlConnection(AppConfig.GetConnectionString()))
            {
                connection.Open();
                var commandStr = @$"IF OBJECT_ID(N'dbo.Products', N'U') IS NULL
                                        CREATE TABLE dbo.Products (
                                            ID INT IDENTITY(1,1) PRIMARY KEY,
	                                        Name VARCHAR(128) UNIQUE,
	                                        Price FLOAT,
	                                        Quantity INT);";
                using (var command = new SqlCommand(commandStr, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
using Microsoft.Extensions.Configuration;

namespace InventoryManagementSystem;

public static class AppConfig
{
    private static IConfiguration? _iconfiguration;

    static AppConfig()
    {
        GetAppSettingsFile();
    }

    private static void GetAppSettingsFile()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        _iconfiguration = builder.Build();
    }

    public static string? GetConnectionString(string databaseType)
    {
        return _iconfiguration?.GetSection($"DatabaseSettings:{databaseType}")["ConnectionString"];
    }
}
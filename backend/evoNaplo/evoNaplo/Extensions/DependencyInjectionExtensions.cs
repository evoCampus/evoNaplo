using evoNaplo.Data;
using evoNaplo.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Xml.Linq;

namespace evoNaplo.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IExampleService, ExampleService>();
        return services;
    }

    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        var host = configuration["EVONAPLO_DB_HOST"];
        var dbName = configuration["EVONAPLO_DB_NAME"];
        var port = configuration["EVONAPLO_DB_PORT"];
        var userName = configuration["EVONAPLO_DB_USER"];
        var pass = configuration["EVONAPLO_DB_PASS"];

        string connectionString;

        if (string.IsNullOrEmpty(host)) throw new Exception("Host was not found!");
        if (string.IsNullOrEmpty(dbName)) throw new Exception("Database name was not found!");
        if (string.IsNullOrEmpty(userName)) throw new Exception("User was not found!");
        if (string.IsNullOrEmpty(pass)) throw new Exception("Password was not found!");


        if (string.IsNullOrEmpty(port))
        {
            port = "1433";
        }

            connectionString = $"Server={host};Database={dbName};User Id={userName};Password={pass};TrustServerCertificate=True;";


        services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

        return services;
    }
}
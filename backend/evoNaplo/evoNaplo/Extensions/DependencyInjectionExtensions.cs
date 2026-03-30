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
        var userName = configuration["EVONAPLO_DB_USER"];
        var pass = configuration["EVONAPLO_DB_PASS"];

        string ConnectionString;
        if (string.IsNullOrEmpty(host))
        {
            host = "(localdb)\\mssqllocaldb";
            dbName = "evoNaploDb";
        }

        if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(pass))
        {
            ConnectionString = $"Server={host};Database={dbName};User Id={userName};Password={pass};TrustServerCertificate=True;";
        }
        else {
            ConnectionString = $"Server={host};Database={dbName};Trusted_Connection=True;TrustServerCertificate=True;";
        }

        services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(ConnectionString));

        return services;
    }
}
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
        var Host = configuration["EVONAPLO_DB_HOST"];
        var DbName = configuration["EVONAPLO_DB_NAME"];
        var UserName = configuration["EVONAPLO_DB_USER"];
        var Pass = configuration["EVONAPLO_DB_PASS"];

        string ConnectionString;
        if (string.IsNullOrEmpty(Host))
        {
            Host = "(localdb)\\mssqllocaldb";
            DbName = "evoNaploDb";
        }

        if (!string.IsNullOrEmpty(UserName))
        {
            ConnectionString = $"Server={Host};Database={DbName};User Id={UserName};Password={Pass};TrustServerCertificate=True;";
        }
        else {
            ConnectionString = $"Server={Host};Database={DbName};Trusted_Connection=True;TrustServerCertificate=True;";
        }

        services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(ConnectionString));

        return services;
    }
}
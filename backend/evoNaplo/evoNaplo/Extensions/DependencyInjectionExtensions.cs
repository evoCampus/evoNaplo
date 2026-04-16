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
        
        services.AddScoped<IMentorService, MentorService>();
        services.AddScoped<ITeamService, TeamService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IStudentService, StudentService>();
        
        services.AddScoped<IExcelImportService, ExcelImportService>();
        services.AddScoped<IExcelExportService, ExcelExportService>();
        services.AddScoped<ICsvImportService, CsvImportService>();
        services.AddScoped<ICsvExportService, CsvExportService>();
        return services;
    }

    public static IServiceCollection AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        var host = configuration["EVONAPLO_DATABASE_HOST"];
        var dbName = configuration["EVONAPLO_DATABASE_PORT"];
        var port = configuration["EVONAPLO_DATABASE_NAME"];
        var userName = configuration["EVONAPLO_DATABASE_USER"];
        var pass = configuration["EVONAPLO_DATABASE_PASS"];

        string connectionString;

        if (string.IsNullOrEmpty(host)) throw new Exception("Host was not found!");
        if (string.IsNullOrEmpty(dbName)) throw new Exception("Database name was not found!");
        if (string.IsNullOrEmpty(userName)) throw new Exception("User was not found!");
        if (string.IsNullOrEmpty(pass)) throw new Exception("Password was not found!");

        if (string.IsNullOrEmpty(port))
        {
            port = "1433";
        }

        connectionString = $"Server={host},{port};Database={dbName};User Id={userName};Password={pass};TrustServerCertificate=True;";

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}
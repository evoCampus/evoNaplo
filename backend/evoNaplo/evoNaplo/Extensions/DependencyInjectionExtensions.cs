using evoNaplo.Services;
using Microsoft.Extensions.DependencyInjection;

namespace evoNaplo.Extensions;
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IExampleService, ExampleService>();
        services.AddScoped<IExcelImportService, ExcelImportService>(); // Injection for Excel Import support
        services.AddScoped<IExcelExportService, ExcelExportService>(); // Injection for Excel Export support
        return services;
    }

    public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
    {
        // To be implemented
        return services;
    }
}
using evoNaplo.Services;

namespace evoNaplo.Extensions;
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IExampleService, ExampleService>();
        
        // Injections for spreadsheet import and export (.xlsx and .csv)
        services.AddScoped<IExcelImportService, ExcelImportService>();
        services.AddScoped<IExcelExportService, ExcelExportService>();
        services.AddScoped<ICsvImportService, CsvImportService>();
        services.AddScoped<ICsvExportService, CsvExportService>();
        return services;
    }

    public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
    {
        // To be implemented
        return services;
    }
}
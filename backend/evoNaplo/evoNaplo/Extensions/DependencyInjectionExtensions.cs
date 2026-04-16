using evoNaplo.Services;

namespace evoNaplo.Extensions;
public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IExampleService, ExampleService>();
        return services;
    }

    {
        return services;
    }
}
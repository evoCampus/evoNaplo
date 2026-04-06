using evoNaplo.Services;
using Microsoft.Extensions.DependencyInjection;
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
        return services;
    }

    public static IServiceCollection AddDatabaseServices(this IServiceCollection services)
    {
        // To be implemented
        return services;
    }
}
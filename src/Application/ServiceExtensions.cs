using DucksAndDogs.Application.Services;
using DucksAndDogs.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DucksAndDogs.Application;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IModelService, ModelService>();

        return services;
    }
}
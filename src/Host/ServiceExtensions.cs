using DucksAndDogs.Core.Stores;
using DucksAndDogs.Persistence.Stores;

namespace DucksAndDogs.Persistence;

public static class StoreServiceExtensions
{
    public static IServiceCollection AddStores(this IServiceCollection services)
    {
        services.AddScoped<IModelStore, ModelStore>();

        return services;
    }
}
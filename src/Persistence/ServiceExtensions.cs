using DucksAndDogs.Core.Stores;
using DucksAndDogs.Persistence.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DucksAndDogs.Persistence;

public static class StoreServiceExtensions
{
    public static IServiceCollection AddStores(this IServiceCollection services)
    {
        services.AddStore<IModelStore, ModelStore>();

        return services;
    }

    private static void AddStore<TInterface, TImplementation>(this IServiceCollection services) 
        where TInterface: class
        where TImplementation : class, IInitializableStore, TInterface
    {
        services.AddScoped<TInterface, TImplementation>();
        services.AddScoped<IInitializableStore, TImplementation>();
    }

    public static IApplicationBuilder InitializeStores(this IApplicationBuilder app)
    {
        var stores = app.ApplicationServices.GetServices<IInitializableStore>();
        foreach (var store in stores) store.InitializeStoreAsync();

        return app;
    }
}
using DucksAndDogs.Application.ML.Services;
using DucksAndDogs.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DucksAndDogs.Application.ML;

public static class MachineLearningServiceExtensions {
    public static IServiceCollection AddMachineLearningServices(this IServiceCollection services) 
    {
        services.AddScoped<IModelTrainingService, ModelTrainingService>();
        services.AddScoped<IModelInferenceService, ModelInferenceService>();

        return services;    
    }
}
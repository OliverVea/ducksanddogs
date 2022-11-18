using DucksAndDogs.Jobs.Jobs;
using Microsoft.Extensions.DependencyInjection;

namespace DucksAndDogs.Jobs;

public static class JobRegistrationExtension {
    public static IServiceCollection AddJobs(this IServiceCollection services) {
        services.AddHostedService<TrainingJob>();

        return services;
    }
}
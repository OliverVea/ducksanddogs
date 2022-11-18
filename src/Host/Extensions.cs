using System.Text.Json.Serialization;
using DucksAndDogs.Application;
using DucksAndDogs.Application.ML;
using DucksAndDogs.Jobs;
using DucksAndDogs.Persistence;

namespace DucksAndDogs.Host;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        services.AddMvc();
        services.AddSwaggerGen(options =>
        {
            options.UseAllOfToExtendReferenceSchemas();
            var libFiles = Directory.EnumerateFiles(AppContext.BaseDirectory, "*.xml");
            foreach (var libFile in libFiles) options.IncludeXmlComments(libFile);
        });

        services.AddApplicationServices()
            .AddMachineLearningServices()
            .AddStores()
            .AddJobs();

        return services;
    }

    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        app.Logger.LogInformation("App is in development: {Development}", app.Environment.IsDevelopment());

        app = app.Environment.IsDevelopment() switch
        {
            true => (WebApplication)app.UseDeveloperExceptionPage(),
            false => app.ConfigureForProduction()
        };

        app.ConfigureSwagger();

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.InitializeStores();

        return app;
    }

    private static WebApplication ConfigureForProduction(this WebApplication app)
    {
        app.UseExceptionHandler("/Error");
        app.UseHsts();

        return app;
    }

    private static WebApplication ConfigureSwagger(this WebApplication app)
    {
        app.UseSwagger(options =>
        {

        });

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ducks&Dogs API");
        });

        return app;
    }
}
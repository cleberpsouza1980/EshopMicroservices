using BuildingBlocks.Exceptions.Handler;

namespace OrderingAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddOrderingAPIServices(this IServiceCollection services)
    {
        // Register API services here
        services.AddCarter();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddHealthChecks();

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        // Configure middleware here
        app.MapCarter();

        app.UseExceptionHandler(options => { });

        app.UseHealthChecks("/health");

        return app;

    }
}

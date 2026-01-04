namespace OrderingAPI;

public static class DependencyInjection
{
    public static IServiceCollection AddOrderingAPIServices(this IServiceCollection services)
    {
        // Register API services here
        //services.AddCarter();
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        // Configure middleware here
        //app.MapCarter();
        return app;

    }
}

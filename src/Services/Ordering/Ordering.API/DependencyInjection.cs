using BuildingBlocks.Extensions;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();

        services.AddCarterForAssembly(currentAssembly);

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();

        return app;
    }
}

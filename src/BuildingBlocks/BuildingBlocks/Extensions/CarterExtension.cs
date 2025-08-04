using Carter;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Extensions;

public static class CarterExtension
{
    public static IServiceCollection AddCarterForAssembly(this IServiceCollection services, Assembly? targetAssembly)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();
        var dependencyCatalog = new DependencyContextAssemblyCatalog(currentAssembly, targetAssembly);

        services.AddCarter(dependencyCatalog);

        return services;
    }
}


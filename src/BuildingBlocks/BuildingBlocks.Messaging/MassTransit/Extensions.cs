using MassTransit;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class Extensions
{
    public static IServiceCollection AddMessageBroker(this IServiceCollection services,
                                                      IConfiguration appConfig,
                                                      Assembly? assembly = null)
    {
        services.AddMassTransit(config =>
        {
            config.SetKebabCaseEndpointNameFormatter();

            if (assembly != null)
            {
                config.AddConsumers(assembly);
            }

            config.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(new Uri(appConfig["MessageBroker:Host"]!), host =>
                {
                    host.Username(appConfig["MessageBroker:UserName"]!);
                    host.Password(appConfig["MessageBroker:Password"]!);
                });
                configurator.ConfigureEndpoints(context);
            });
        });

        return services;
    }
}


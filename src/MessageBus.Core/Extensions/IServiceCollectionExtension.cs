using System.Reflection;
using MassTransit;
using MessageBus.Core.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MessageBus.Core.Extensions;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddMassTransitWithRabbitMq(this IServiceCollection services)
    {
        services.AddMassTransit(config =>
        {
            config.AddConsumers(Assembly.GetEntryAssembly());

            config.UsingRabbitMq((context, cfg) =>
            {
                var rabbitMqSetting = context.GetRequiredService<IOptions<RabbitMqSetting>>().Value;

                cfg.Host(rabbitMqSetting.Host, "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("", true));
            });
        });

        return services;
    }
}

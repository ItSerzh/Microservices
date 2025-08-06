using MassTransit;
using Microsoft.FeatureManagement;
using Ordering.Domain.Events.Domain;

namespace Ordering.Application.Orders.Events.Domain;

public class OrderCreatedEventHandler(IPublishEndpoint publishEnpoint, IFeatureManager featureManager,
                                      ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Domain Event Handled {domainEvent.GetType().Name}");

        if (await featureManager.IsEnabledAsync("OrderFullfilment"))
        {
            var orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDto();
            await publishEnpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
        }
    }
}


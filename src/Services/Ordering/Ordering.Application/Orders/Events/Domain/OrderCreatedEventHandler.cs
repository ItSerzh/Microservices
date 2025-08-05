using Ordering.Domain.Events.Domain;

namespace Ordering.Application.Orders.Events.Domain;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<OrderCreatedEvent>
{
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handled {notification.GetType().Name}");

        return Task.CompletedTask;
    }
}


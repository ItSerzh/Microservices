namespace Ordering.Application.Orders.Events.Domain;

public class OrderUpdatedEventHandler(ILogger<OrderUpdatedEventHandler> logger)
    : INotificationHandler<OrderUpdatedEvent>
{
    public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Handled {notification.GetType().Name}");
        
        return Task.CompletedTask;
    }
}


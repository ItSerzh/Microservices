namespace Ordering.Domain.Events.Domain;

public record OrderCreatedEvent(Order Order) : IDomainEvent;


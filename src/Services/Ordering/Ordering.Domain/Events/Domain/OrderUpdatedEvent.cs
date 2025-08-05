namespace Ordering.Domain.Events.Domain;

public record OrderUpdatedEvent(Order Order) : IDomainEvent;


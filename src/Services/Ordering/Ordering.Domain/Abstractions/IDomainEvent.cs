using MediatR;

namespace Ordering.Domain.Abstractions;

public interface IDomainEvent : INotification
{
    Guid EventId => Guid.NewGuid();

    DateTimeOffset HappendOn => DateTimeOffset.UtcNow;

    string EventType => GetType().AssemblyQualifiedName;
}


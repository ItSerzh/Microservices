namespace BuildingBlocks.Messaging.Events;

public record IntegrationEvent
{
    Guid EventId => Guid.NewGuid();

    DateTimeOffset HappendOn => DateTimeOffset.UtcNow;

    string EventType => GetType().AssemblyQualifiedName;
}


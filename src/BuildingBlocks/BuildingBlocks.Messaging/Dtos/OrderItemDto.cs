namespace BuildingBlocks.Messaging.Dtos;
public record OrderItemDto(
    Guid OrderId,
    Guid ProductId,
    int Quantity,
    decimal Price);
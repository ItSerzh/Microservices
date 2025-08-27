using BuildingBlocks.Messaging.Dtos;
using BuildingBlocks.Messaging.Enums;

namespace BuildingBlocks.Messaging.Events;

public record ShoppingCartCheckoutEvent(
    Guid Id,
    Guid CustomerId,
    string OrderName,
    AddressDto Shipping,
    AddressDto Billing,
    PaymentDto Payment,
    OrderStatus Status,
    List<OrderItemDto> OrderItems) : IntegrationEvent;


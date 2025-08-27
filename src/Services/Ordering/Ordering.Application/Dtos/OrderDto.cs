using BuildingBlocks.Messaging.Dtos;
using Ordering.Domain.Enums;

namespace Ordering.Application.Dtos;

public record class OrderDto(
    Guid Id,
    Guid CustomerId,
    string OrderName,
    AddressDto Shipping,
    AddressDto Billing,
    PaymentDto Payment,
    OrderStatus Status,
    List<OrderItemDto> OrderItems);


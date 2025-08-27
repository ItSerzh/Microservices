using BuildingBlocks.Messaging.Dtos;
using ShoppingCart.API.Enums;

namespace ShoppingCart.API.Dtos;

public record CheckoutDto
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public string OrderName { get; init; }
    public AddressDto Shipping { get; init; }
    public AddressDto Billing { get; init; }
    public PaymentDto Payment { get; init; }
    public OrderStatus Status { get; init; }

    public List<OrderItemDto> OrderItems = [];
}

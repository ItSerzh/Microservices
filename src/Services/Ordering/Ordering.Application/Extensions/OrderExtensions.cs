using BuildingBlocks.Messaging.Dtos;

namespace Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToOrderDto(this IEnumerable<Order> orders)
    {
        var result = orders.Select(ToOrderDto);

        return result;
    }

    public static OrderDto ToOrderDto(this Order order)
    {
        var result = new OrderDto(
                order.Id.Value,
                order.CustomerId.Value,
                order.OrderName.Value,
                new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress,
                               order.ShippingAddress.AddressLine, order.ShippingAddress.Contry, order.ShippingAddress.State,
                               order.ShippingAddress.ZipCode),
                new AddressDto(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress,
                               order.BillingAddress.AddressLine, order.BillingAddress.Contry, order.BillingAddress.State,
                               order.BillingAddress.ZipCode),
                new PaymentDto(order.Payment.CardName, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.Cvv,
                               order.Payment.PaymentMethod),
                order.OrderStatus,
                order.OrderItems.Select(i => new OrderItemDto(
                    i.OrderId.Value,
                    i.ProductId.Value,
                    i.Quantity,
                    i.Price)).ToList()
                );

        return result;
    }
}


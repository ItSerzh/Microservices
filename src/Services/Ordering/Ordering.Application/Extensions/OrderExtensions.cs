namespace Ordering.Application.Extensions;

public static class OrderExtensions
{
    public static IEnumerable<OrderDto> ToOrderDto(this IEnumerable<Order> orders)
    {
        var result = orders.Select(o => new OrderDto(
                o.Id.Value,
                o.CustomerId.Value,
                o.OrderName.Value,
                new AddressDto(
                    o.ShippingAddress.FirstName,
                    o.ShippingAddress.LastName,
                    o.ShippingAddress.EmailAddress,
                    o.ShippingAddress.AddressLine,
                    o.ShippingAddress.Contry,
                    o.ShippingAddress.State,
                    o.ShippingAddress.ZipCode),
                new AddressDto(
                    o.BillingAddress.FirstName,
                    o.BillingAddress.LastName,
                    o.BillingAddress.EmailAddress,
                    o.BillingAddress.AddressLine,
                    o.BillingAddress.Contry,
                    o.BillingAddress.State,
                    o.BillingAddress.ZipCode),
                new PaymentDto(
                    o.Payment.CardName,
                    o.Payment.CardNumber,
                    o.Payment.Expiration,
                    o.Payment.Cvv,
                    o.Payment.PaymentMethod),
                o.OrderStatus,
                o.OrderItems.Select(i => new OrderItemDto(
                    i.OrderId.Value,
                    i.ProductId.Value,
                    i.Quantity,
                    i.Price)).ToList()
                ));

        return result;
    }
}


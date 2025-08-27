namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderHandler(IOrderingDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
{
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var order = CreatNewOrder(command.Order);

        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateOrderResult(order.Id.Value);
    }

    private static Order CreatNewOrder(OrderDto order)
    {
        var shipping = Address.Off(order.Shipping.FirstName,
                                   order.Shipping.LastName,
                                   order.Shipping.EmailAddress,
                                   order.Shipping.AddressLine,
                                   order.Shipping.Contry,
                                   order.Shipping.State,
                                   order.Shipping.ZipCode);

        var billing = Address.Off(order.Billing.FirstName,
                                   order.Billing.LastName,
                                   order.Billing.EmailAddress,
                                   order.Billing.AddressLine,
                                   order.Billing.Contry,
                                   order.Billing.State,
                                   order.Billing.ZipCode);

        var newOrder = Order.Create(
            OrderId.Of(Guid.NewGuid()),
            CustomerId.Of(order.CustomerId),
            OrderName.Of(order.OrderName),
            shipping,
            billing,
            Payment.Of(order.Payment.CardName, order.Payment.CardNumber, order.Payment.Expiration, order.Payment.Cvv, order.Payment.PaymentMethod));

        foreach (var item in order.OrderItems)
        {
            newOrder.Add(ProductId.Of(item.ProductId), item.Quantity, item.Price);
        }

        return newOrder;
    }
}


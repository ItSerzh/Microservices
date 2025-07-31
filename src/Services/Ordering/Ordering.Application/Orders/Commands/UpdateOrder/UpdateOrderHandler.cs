namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderHandler(IOrderingDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
{
    public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.Order.Id);

        var existingOrder = await dbContext.Orders.FindAsync(orderId, cancellationToken);

        if (existingOrder is null)
        {
            throw new OrderNotFoundException(command.Order.Id);
        }

        UpdateOrder(existingOrder, command.Order);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateOrderResult(true);
    }

    private void UpdateOrder(Order existingOrder, OrderDto order)
    {
        var shipping = Address.Off(order.Shipping.FirstName, order.Shipping.LastName, order.Shipping.EmailAddress,
                                   order.Shipping.AddressLine, order.Shipping.Contry, order.Shipping.State,
                                   order.Shipping.ZipCode);

        var billing = Address.Off(order.Billing.FirstName, order.Billing.LastName, order.Billing.EmailAddress,
                                  order.Billing.AddressLine, order.Billing.Contry, order.Billing.State,
                                  order.Billing.ZipCode);

        var payment = Payment.Of(order.Payment.CardName, order.Payment.CardNumber, order.Payment.Expiration,
                                 order.Payment.Cvv, order.Payment.PaymentMethod);

        existingOrder.Update(CustomerId.Of(order.CustomerId), OrderName.Of(order.OrderName), shipping, billing, payment,
                             order.Status);
    }
}


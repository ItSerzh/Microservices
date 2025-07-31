namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderHandler(IOrderingDbContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
{
    public async Task<DeleteOrderResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
    {
        var orderId = OrderId.Of(command.Id);

        var existingOrder = await dbContext.Orders.FindAsync(orderId, cancellationToken);

        if (existingOrder is null)
        {
            throw new OrderNotFoundException(command.Id);
        }

        dbContext.Orders.Remove(existingOrder);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteOrderResult(true);
    }
}


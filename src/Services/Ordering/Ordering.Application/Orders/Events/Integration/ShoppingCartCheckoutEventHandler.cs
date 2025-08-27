using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain.Enums;

namespace Ordering.Application.Orders.Events.Integration;

public class ShoppingCartCheckoutEventHandler(ISender sender, ILogger<ShoppingCartCheckoutEventHandler> logger)
    : IConsumer<ShoppingCartCheckoutEvent>
{
    public async Task Consume(ConsumeContext<ShoppingCartCheckoutEvent> context)
    {
        logger.LogInformation($"Intergation Event handled: {context}");

        var command = MapToCreatedOrderCommand(context.Message);
        await sender.Send(command);
    }

    private CreateOrderCommand MapToCreatedOrderCommand(ShoppingCartCheckoutEvent message)
    {
        var orderId = Guid.NewGuid();

        var orderDto = new OrderDto(
                Id: orderId,
                message.CustomerId,
                message.OrderName,
                message.Shipping,
                message.Billing,
                message.Payment,
                OrderStatus.Pending,
                message.OrderItems);

        return new CreateOrderCommand(orderDto);
    }
}


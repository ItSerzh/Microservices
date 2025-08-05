using BuildingBlocks.Messaging.Events;
using MassTransit;
using Ordering.Application.Orders.Commands.CreateOrder;
using Ordering.Domain.Enums;

namespace Ordering.Application.Orders.Events.Integration;

internal class ShoppingCartCheckoutEventHandler(ISender sender, ILogger<ShoppingCartCheckoutEventHandler> logger)
    : IConsumer<ShoppingCartCheckoutEvent>
{
    public async Task Consume(ConsumeContext<ShoppingCartCheckoutEvent> context)
    {
        logger.LogInformation($"Intergation Event handled: {context}");

        var command = MapToCreatedOrderCommand(context.Message);
        await sender.Send(command);
    }

    private IRequest<object> MapToCreatedOrderCommand(ShoppingCartCheckoutEvent message)
    {
        var addressDto = new AddressDto(message.FirstName, message.LastName, message.EmailAddress, message.AddressLine, message.Contry, message.State, message.ZipCode);
        var paymentDto = new PaymentDto(message.CardName, message.CardNumber, message.Expiration, message.Cvv, message.PaymentMethod);
        var orderId = new Guid();

        var orderDto = new OrderDto(
                Id: orderId,
                message.CustomerId,
                message.Username,
                addressDto,
                addressDto,
                paymentDto,
                OrderStatus.Pending,
                [
                    new OrderItemDto(orderId, new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"), 1, 950.0m),
                    new OrderItemDto(orderId, new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"), 2, 840.0m)
                ]);

        return new CreateOrderCommand(orderDto);
    }
}


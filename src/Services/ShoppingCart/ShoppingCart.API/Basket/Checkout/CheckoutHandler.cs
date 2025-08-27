using BuildingBlocks.Messaging.Dtos;
using BuildingBlocks.Messaging.Events;
using MassTransit;

namespace ShoppingCart.API.Basket.Checkout;

public record CheckoutCommand(CheckoutDto CheckoutDto) : ICommand<CheckoutResult>;

public record CheckoutResult(bool IsSuccess);

public class CheckoutHandler(IShoppingCartRepository cartRepository, IPublishEndpoint publishEnpoint)
    : ICommandHandler<CheckoutCommand, CheckoutResult>
{
    public async Task<CheckoutResult> Handle(CheckoutCommand command, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetShoppingTrolley(command.CheckoutDto.CustomerId, cancellationToken);

        if (cart == null)
        {
            return new CheckoutResult(false);
        }

        var orderItems = cart.Items.Adapt<List<OrderItemDto>>();
        command.CheckoutDto.OrderItems =  orderItems;
        var eventMessage = command.CheckoutDto.Adapt<ShoppingCartCheckoutEvent>();

        await publishEnpoint.Publish(eventMessage, cancellationToken);

        await cartRepository.DeleteShoppingTroley(cart.Id, cancellationToken);

        return new CheckoutResult(true);
    }
}

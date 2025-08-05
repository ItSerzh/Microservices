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
        var cart = await cartRepository.GetShoppingTrolley(command.CheckoutDto.Username, cancellationToken);

        if (cart == null)
        {
            return new CheckoutResult(false);
        }

        var eventMessage = command.CheckoutDto.Adapt<ShoppingCartCheckoutEvent>();
        eventMessage.TotalPrice = cart.TotalPrice;

        await publishEnpoint.Publish(eventMessage, cancellationToken);

        await cartRepository.DeleteShoppingTroley(cart.Username, cancellationToken);

        return new CheckoutResult(true);
    }
}

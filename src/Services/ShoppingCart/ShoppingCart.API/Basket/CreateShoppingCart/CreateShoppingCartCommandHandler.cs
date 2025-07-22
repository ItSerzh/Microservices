
namespace ShoppingCart.API.Basket.CreateShoppingCart;

public record CreateShoppingCartCommand(ShoppingTrolley ShoppingTrolley) : ICommand<CreateShoppingCartResult>;

public record CreateShoppingCartResult(string Username);

public class CreateShoppingCartCommandHandler : ICommandHandler<CreateShoppingCartCommand, CreateShoppingCartResult>
{
    public async Task<CreateShoppingCartResult> Handle(CreateShoppingCartCommand command, CancellationToken cancellationToken)
    {
        var cart = command.ShoppingTrolley;

        return new CreateShoppingCartResult("some Username");
    }
}

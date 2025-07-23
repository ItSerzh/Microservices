namespace ShoppingCart.API.Basket.CreateShoppingCart;

public record CreateShoppingCartCommand(ShoppingTrolley ShoppingTrolley) : ICommand<CreateShoppingCartResult>;

public record CreateShoppingCartResult(string Username);

public class CreateShoppingCartCommandHandler(IShoppingCartRepository repository) : ICommandHandler<CreateShoppingCartCommand, CreateShoppingCartResult>
{
    public async Task<CreateShoppingCartResult> Handle(CreateShoppingCartCommand command, CancellationToken cancellationToken)
    {
        var trolley = command.ShoppingTrolley;

        var result = await repository.CreateShoppingTrolley(trolley, cancellationToken);

        return new CreateShoppingCartResult(result.Username);
    }
}

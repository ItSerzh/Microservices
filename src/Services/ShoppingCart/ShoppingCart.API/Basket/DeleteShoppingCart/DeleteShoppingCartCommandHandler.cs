namespace ShoppingCart.API.Basket.DeleteShoppingCart;

public record DeleteShoppingCartCommand(string Username) : ICommand<DeleteShoppingCartResult>;

public record DeleteShoppingCartResult(bool IsSuccess);

public class DeleteShoppingCartCommandHandler(IShoppingCartRepository repository) : ICommandHandler<DeleteShoppingCartCommand, DeleteShoppingCartResult>
{
    public async Task<DeleteShoppingCartResult> Handle(DeleteShoppingCartCommand request, CancellationToken cancellationToken)
    {
        var result = await repository.DeleteShoppingTroley(request.Username, cancellationToken);
        
        return new DeleteShoppingCartResult(result);
    }
}

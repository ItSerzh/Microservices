
namespace ShoppingCart.API.Basket.DeleteShoppingCart;

public record DeleteShoppingCartCommand(string Username) : ICommand<DeleteShoppingCartResult>;

public record DeleteShoppingCartResult(bool IsSuccess);

public class DeleteShoppingCartCommandHandler : ICommandHandler<DeleteShoppingCartCommand, DeleteShoppingCartResult>
{
    public async Task<DeleteShoppingCartResult> Handle(DeleteShoppingCartCommand request, CancellationToken cancellationToken)
    {
        return new DeleteShoppingCartResult(true);
    }
}

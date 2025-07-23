namespace ShoppingCart.API.Data;

public class ShoppingCartRepository(IDocumentSession documentSession) : IShoppingCartRepository
{
    public async Task<ShoppingTrolley> CreateShoppingTrolley(ShoppingTrolley shoppingTrolley, CancellationToken cancellationToken)
    {
        documentSession.Store(shoppingTrolley);
        await documentSession.SaveChangesAsync(cancellationToken);
        
        return shoppingTrolley;
    }

    public async Task<bool> DeleteShoppingTroley(string username, CancellationToken cancellationToken)
    {
        documentSession.Delete<ShoppingTrolley>(username);
        await documentSession.SaveChangesAsync(cancellationToken);
        
        return true;
    }

    public async Task<ShoppingTrolley> GetShoppingTrolley(string username, CancellationToken cancellationToken)
    {
        var shoppingCart = await documentSession.LoadAsync<ShoppingTrolley>(username, cancellationToken);
        
        return shoppingCart ?? throw new ShoppingTrolleyNotFoundException(username);
    }
}

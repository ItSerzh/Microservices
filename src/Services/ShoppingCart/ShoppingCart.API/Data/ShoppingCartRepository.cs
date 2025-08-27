namespace ShoppingCart.API.Data;

public class ShoppingCartRepository(IDocumentSession documentSession) : IShoppingCartRepository
{
    public async Task<ShoppingTrolley> CreateShoppingTrolley(ShoppingTrolley shoppingTrolley, CancellationToken cancellationToken)
    {
        documentSession.Store(shoppingTrolley);
        await documentSession.SaveChangesAsync(cancellationToken);
        
        return shoppingTrolley;
    }

    public async Task<bool> DeleteShoppingTroley(Guid userId, CancellationToken cancellationToken)
    {
        documentSession.Delete<ShoppingTrolley>(userId);
        await documentSession.SaveChangesAsync(cancellationToken);
        
        return true;
    }

    public async Task<ShoppingTrolley> GetShoppingTrolley(Guid userId, CancellationToken cancellationToken)
    {
        var shoppingCart = await documentSession.LoadAsync<ShoppingTrolley>(userId, cancellationToken);
        
        return shoppingCart ?? throw new ShoppingTrolleyNotFoundException(userId.ToString());
    }
}

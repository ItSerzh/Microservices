namespace ShoppingCart.API.Data;

public interface IShoppingCartRepository
{
    Task<ShoppingTrolley> GetShoppingTrolley(Guid userId, CancellationToken cancellationToken);
    
    Task<ShoppingTrolley> CreateShoppingTrolley(ShoppingTrolley shoppingTrolley, CancellationToken cancellationToken);

    Task<bool> DeleteShoppingTroley(Guid userId, CancellationToken cancellationToken);
}

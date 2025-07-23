namespace ShoppingCart.API.Data;

public interface IShoppingCartRepository
{
    Task<ShoppingTrolley> GetShoppingTrolley(string username, CancellationToken cancellationToken);
    
    Task<ShoppingTrolley> CreateShoppingTrolley(ShoppingTrolley shoppingTrolley, CancellationToken cancellationToken);

    Task<bool> DeleteShoppingTroley(string username, CancellationToken cancellationToken);
}

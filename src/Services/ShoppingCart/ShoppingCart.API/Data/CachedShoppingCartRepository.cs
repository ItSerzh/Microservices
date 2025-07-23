using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace ShoppingCart.API.Data;

public class CachedShoppingCartRepository(IShoppingCartRepository repository, IDistributedCache cache) : IShoppingCartRepository
{
    public async Task<ShoppingTrolley> CreateShoppingTrolley(ShoppingTrolley shoppingTrolley, CancellationToken cancellationToken)
    {
        await repository.CreateShoppingTrolley(shoppingTrolley, cancellationToken);

        await cache.SetStringAsync(shoppingTrolley.Username, JsonSerializer.Serialize(shoppingTrolley), cancellationToken);

        return shoppingTrolley;
    }

    public async Task<bool> DeleteShoppingTroley(string username, CancellationToken cancellationToken)
    {
        await repository.DeleteShoppingTroley(username, cancellationToken);

        await cache.RemoveAsync(username, cancellationToken);

        return true;
    }

    public async Task<ShoppingTrolley> GetShoppingTrolley(string username, CancellationToken cancellationToken)
    {
        var cachedString = await cache.GetStringAsync(username, cancellationToken);

        if (!string.IsNullOrEmpty(cachedString))
        {
            return JsonSerializer.Deserialize<ShoppingTrolley>(cachedString)!;
        }

        var trolley = await repository.GetShoppingTrolley(username, cancellationToken);
        await cache.SetStringAsync(username, JsonSerializer.Serialize(trolley), cancellationToken);

        return trolley;
    }
}

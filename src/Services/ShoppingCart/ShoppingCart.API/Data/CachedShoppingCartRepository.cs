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

    public async Task<bool> DeleteShoppingTroley(Guid userId, CancellationToken cancellationToken)
    {
        await repository.DeleteShoppingTroley(userId, cancellationToken);

        await cache.RemoveAsync(userId.ToString(), cancellationToken);

        return true;
    }

    public async Task<ShoppingTrolley> GetShoppingTrolley(Guid userId, CancellationToken cancellationToken)
    {
        var cachedString = await cache.GetStringAsync(userId.ToString(), cancellationToken);

        if (!string.IsNullOrEmpty(cachedString))
        {
            return JsonSerializer.Deserialize<ShoppingTrolley>(cachedString)!;
        }

        var trolley = await repository.GetShoppingTrolley(userId, cancellationToken);
        await cache.SetStringAsync(userId.ToString(), JsonSerializer.Serialize(trolley), cancellationToken);

        return trolley;
    }
}

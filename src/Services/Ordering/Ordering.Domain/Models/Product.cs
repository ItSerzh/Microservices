namespace Ordering.Domain.Models;

public class Product : Entity<ProductId>
{
    public static Product Create(ProductId productId, string name, int price)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

        var product = new Product
        {
            Id = productId,
            Name = name,
            Price = price
        };

        return product;
    }

    public string Name { get; private set; } = default!;

    public int Price { get; private set; }
}


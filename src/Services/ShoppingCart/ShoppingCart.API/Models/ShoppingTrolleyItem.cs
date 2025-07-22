namespace ShoppingCart.API.Models;

public class ShoppingTrolleyItem
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; } = default!;

    public string Color { get; set; } = default!;

    public decimal Price { get; set; } = default!;

    public int Quantity { get; set; } = default!;
}

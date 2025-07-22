namespace ShoppingCart.API.Models;

public class ShoppingTrolley
{
    public string Username { get; set; }

    public List<ShoppingTrolleyItem> Items { get; set; } = [];

    public decimal TotalPrice => Items.Sum(i => i.Quantity * i.Price);

    
    public ShoppingTrolley(string username) => Username = username;

    //requres for mapping
    public ShoppingTrolley() { }
}

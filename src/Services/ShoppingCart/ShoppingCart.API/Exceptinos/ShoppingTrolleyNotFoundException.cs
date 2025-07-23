namespace ShoppingCart.API.Exceptinos;

public class ShoppingTrolleyNotFoundException(string username) : NotFoundException(username);

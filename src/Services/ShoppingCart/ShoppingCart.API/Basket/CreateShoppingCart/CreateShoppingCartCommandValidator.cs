namespace ShoppingCart.API.Basket.CreateShoppingCart;

public class CreateShoppingCartCommandValidator : AbstractValidator<CreateShoppingCartCommand>
{
    public CreateShoppingCartCommandValidator()
    {
        RuleFor(c => c.ShoppingTrolley).NotNull().WithMessage("Shopping cart can't be null");
        RuleFor(c => c.ShoppingTrolley.Username).NotEmpty().WithMessage("Username is requred");
    }
}

namespace ShoppingCart.API.Basket.DeleteShoppingCart;

public class DeleteShoppingCartCommandValidator : AbstractValidator<DeleteShoppingCartCommand>
{
    public DeleteShoppingCartCommandValidator()
    {
        RuleFor(c => c.Username).NotEmpty().WithMessage("Username is requred");
    }
}

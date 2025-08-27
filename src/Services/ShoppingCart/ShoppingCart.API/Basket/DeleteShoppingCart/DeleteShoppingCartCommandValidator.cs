namespace ShoppingCart.API.Basket.DeleteShoppingCart;

public class DeleteShoppingCartCommandValidator : AbstractValidator<DeleteShoppingCartCommand>
{
    public DeleteShoppingCartCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty().WithMessage("UserId is requred");
    }
}

namespace ShoppingCart.API.Basket.Checkout;

public class CheckoutValidator : AbstractValidator<CheckoutCommand>
{
    public CheckoutValidator()
    {
        RuleFor(c => c.CheckoutDto).NotNull().WithMessage("CheckoutDto is required");
        RuleFor(c => c.CheckoutDto.Username).NotEmpty().WithMessage("Username is required");
    }
}

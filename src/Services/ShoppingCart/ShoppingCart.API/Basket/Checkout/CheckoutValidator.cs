namespace ShoppingCart.API.Basket.Checkout;

public class CheckoutValidator : AbstractValidator<CheckoutCommand>
{
    public CheckoutValidator()
    {
        RuleFor(c => c.CheckoutDto).NotNull().WithMessage("CheckoutDto is required");
        RuleFor(c => c.CheckoutDto.CustomerId).NotEmpty().WithMessage("UserId is required");
    }
}

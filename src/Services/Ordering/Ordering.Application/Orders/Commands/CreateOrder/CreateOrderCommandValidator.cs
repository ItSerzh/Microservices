using FluentValidation;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(o => o.Order.OrderName).NotEmpty().WithMessage("Name is required");
        RuleFor(o => o.Order.CustomerId).NotEmpty().WithMessage("CustomerId is required");
        RuleFor(o => o.Order.OrderItems).NotEmpty().WithMessage("OrderItems is required");
    }
}


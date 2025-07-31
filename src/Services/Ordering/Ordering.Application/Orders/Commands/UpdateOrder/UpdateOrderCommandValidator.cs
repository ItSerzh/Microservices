using FluentValidation;

namespace Ordering.Application.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(o => o.Order.OrderName).NotEmpty().WithMessage("Name is required");
        RuleFor(o => o.Order.CustomerId).NotEmpty().WithMessage("CustomerId is required");
        RuleFor(o => o.Order.OrderItems).NotEmpty().WithMessage("OrderItems is required");
    }
}


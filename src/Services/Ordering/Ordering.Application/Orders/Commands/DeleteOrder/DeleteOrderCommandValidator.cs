using FluentValidation;

namespace Ordering.Application.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(d => d.Id).NotEmpty().WithMessage("Order Id is required");
    }
}


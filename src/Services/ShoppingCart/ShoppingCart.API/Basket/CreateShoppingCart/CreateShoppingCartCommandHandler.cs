using Discount.Grpc;

namespace ShoppingCart.API.Basket.CreateShoppingCart;

public record CreateShoppingCartCommand(ShoppingTrolley ShoppingTrolley) : ICommand<CreateShoppingCartResult>;

public record CreateShoppingCartResult(string Username);

public class CreateShoppingCartCommandHandler(IShoppingCartRepository repository,
                                              DiscountProtoService.DiscountProtoServiceClient discountProto)
    : ICommandHandler<CreateShoppingCartCommand, CreateShoppingCartResult>
{
    public async Task<CreateShoppingCartResult> Handle(CreateShoppingCartCommand command, CancellationToken cancellationToken)
    {
        await ApplyDiscount(command.ShoppingTrolley.Items, discountProto, cancellationToken);

        var trolley = command.ShoppingTrolley;

        var result = await repository.CreateShoppingTrolley(trolley, cancellationToken);

        return new CreateShoppingCartResult(result.Username);
    }

    private static async Task ApplyDiscount(List<ShoppingTrolleyItem> items,
                                            DiscountProtoService.DiscountProtoServiceClient discountProto,
                                            CancellationToken cancellationToken)
    {
        foreach (var item in items)
        {
            var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName },
                                                              cancellationToken: cancellationToken);
            item.Price -= coupon.Amount;
        }
    }
}

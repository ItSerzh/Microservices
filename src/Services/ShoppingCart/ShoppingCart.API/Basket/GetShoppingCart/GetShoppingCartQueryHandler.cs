namespace ShoppingCart.API.Basket.GetShoppingCart;

public record GetShoppingCartQuery(string Username) : IQuery<GetShoppingCartResult>;

public record GetShoppingCartResult(ShoppingTrolley ShoppingTrolley);

public class GetShoppingCartQueryHandler() : IQueryHandler<GetShoppingCartQuery, GetShoppingCartResult>
{
    public async Task<GetShoppingCartResult> Handle(GetShoppingCartQuery auery, CancellationToken cancellationToken)
    {
        return new GetShoppingCartResult(new ShoppingTrolley("someUsername"));
    }
}

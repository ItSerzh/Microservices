namespace ShoppingCart.API.Basket.GetShoppingCart;

public record GetShoppingCartQuery(string Username) : IQuery<GetShoppingCartResult>;

public record GetShoppingCartResult(ShoppingTrolley ShoppingTrolley);

public class GetShoppingCartQueryHandler(IShoppingCartRepository repository) : IQueryHandler<GetShoppingCartQuery, GetShoppingCartResult>
{
    public async Task<GetShoppingCartResult> Handle(GetShoppingCartQuery query, CancellationToken cancellationToken)
    {
        var trolley = await repository.GetShoppingTrolley(query.Username, cancellationToken);

        return new GetShoppingCartResult(trolley);
    }
}

namespace ShoppingCart.API.Basket.GetShoppingCart;

public record GetShoppingCartRequest(string Username );

public record GetShoppingCartResponse(ShoppingTrolley ShoppingTrolley);

public class GetShoppingCartEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/shopping-cart/{username}", async (string username, ISender sender) =>
        {
            var result = await sender.Send(new GetShoppingCartQuery(username));

            var response = result.Adapt<GetShoppingCartResponse>();

            return Results.Ok(response);
        })
            .WithName("GetShoppingCart")
            .Produces<GetShoppingCartResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Shoppint Cart")
            .WithDescription("Get Shoppint Cart");

    }
}
